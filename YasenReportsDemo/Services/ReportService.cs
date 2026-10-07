using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using YasenReportsDemo.Data;
using YasenReportsDemo.Models;

namespace YasenReportsDemo.Services;

public class RowViewModel
{
    public string Key { get; set; } = "";
    public string Label { get; set; } = "";
    /// <summary>Итоговые значения колонок (ручной ввод + computed/dict из snapshot)</summary>
    public Dictionary<string, double?> Values { get; set; } = new();
    public List<string> ColumnErrors { get; set; } = new();
}

public class ReportViewModel
{
    public ReportInstance Instance { get; set; } = null!;
    public FormConfig Config { get; set; } = null!;
    public int ConfigVersionId { get; set; }
    public bool IsEditable { get; set; }
    public List<RowViewModel> Rows { get; set; } = new();
    public Dictionary<string, double?> Totals { get; set; } = new();
    public List<ValidationError> Errors { get; set; } = new();
    public List<string> SqlWarnings { get; set; } = new();
    public List<ValidationRule> Rules { get; set; } = new();
}

/// <summary>
/// Оркестрация: создание/чтение отчёта, вычисление SQL-колонок со snapshot-защитой, валидация.
/// Ключевое правило: текущий год — генерируем/редактируем; прошлые годы — только читаем сохранённые снимки.
/// </summary>
public class ReportService
{
    private readonly AppDbContext _db;
    private readonly FormConfigService _cfg;
    private readonly SqlComputedService _sql;
    private readonly ValidationService _val;

    public ReportService(AppDbContext db, FormConfigService cfg, SqlComputedService sql, ValidationService val)
    {
        _db = db; _cfg = cfg; _sql = sql; _val = val;
    }

    public int CurrentYear => DateTime.UtcNow.Year;

    /// <summary>Создать экземпляр отчёта (только для текущего года).</summary>
    public async Task<ReportInstance> CreateAsync(string formKey, int year, int regionId)
    {
        var existing = await _db.ReportInstances.FirstOrDefaultAsync(r => r.FormKey == formKey && r.Year == year && r.RegionId == regionId);
        if (existing is not null) return existing;

        var ver = await _cfg.GetActiveForYearAsync(formKey, year)
                  ?? throw new InvalidOperationException($"Нет конфигурации формы {formKey} на {year} г.");

        var inst = new ReportInstance { FormKey = formKey, Year = year, RegionId = regionId, ConfigVersionId = ver.Id };
        _db.ReportInstances.Add(inst);
        await _db.SaveChangesAsync();
        return inst;
    }

    /// <summary>Получить/создать и собрать ViewModel. Для прошлых лет — без пересчёта SQL (только snapshot).</summary>
    public async Task<ReportViewModel> GetOrCreateAsync(string formKey, int year, int regionId, bool recalcComputed)
    {
        var inst = await _db.ReportInstances
            .Include(r => r.Rows)//.ThenInclude(x => x.ReportInstance)
            .Include(r => r.ConfigVersion)
            .FirstOrDefaultAsync(r => r.FormKey == formKey && r.Year == year && r.RegionId == regionId);

        if (inst is null)
        {
            if (year != CurrentYear)
                return null!; // за прошлый год отчёта нет — показываем «не создан»
            inst = await CreateAsync(formKey, year, regionId);
            inst = await _db.ReportInstances
                .Include(r => r.Rows).Include(r => r.ConfigVersion)
                .FirstAsync(r => r.Id == inst.Id);
        }

        var cfg = FormConfigService.Parse(inst.ConfigVersion!.ConfigJson);
        var vm = new ReportViewModel
        {
            Instance = inst,
            Config = cfg,
            ConfigVersionId = inst.ConfigVersionId,
            IsEditable = inst.Status == "draft" && year == CurrentYear,
            Rules = await _val.GetRulesAsync(cfg.FormKey, year)
        };

        var rowKeys = await _cfg.GetRowKeysAsync(cfg.RowSource, regionId);
        var rowsByKv = inst.Rows.ToDictionary(r => r.RowKey);

        // Новые строки справочника (появились в этом году) — создаём для редактируемых отчётов текущего года
        if (vm.IsEditable)
            foreach (var (key, _) in rowKeys.Where(k => !rowsByKv.ContainsKey(k.Key)))
                rowsByKv[key] = EnsureRow(inst, key, rowKeys.FindIndex(x => x.Key == key));

        foreach (var (key, label) in rowKeys)
        {
            rowsByKv.TryGetValue(key, out var stored);
            if (stored is null) continue; // за прошлые годы новой строки может не быть — просто не показываем
            var snap = ParseJson(stored.ComputedSnapshotJson);
            var manual = ParseJson(stored.ValuesJson);

            var values = new Dictionary<string, double?>();
            foreach (var col in cfg.Columns)
            {
                switch (col.Kind)
                {
                    case ColumnKind.Manual:
                        values[col.Key] = manual.GetValueOrDefault(col.Key);
                        break;
                    case ColumnKind.Dict:
                        // dict тоже «справочник»: значение подставляется при открытии, но сохраняется как value
                        if (!manual.ContainsKey(col.Key) && snap.TryGetValue(col.Key, out var dv))
                            values[col.Key] = dv;
                        else
                            values[col.Key] = manual.GetValueOrDefault(col.Key);
                        break;
                    case ColumnKind.Computed:
                    case ColumnKind.Formula:
                        // Пересчёт SQL только если разрешено (текущий год, draft). Иначе — snapshot.
                        if (recalcComputed && vm.IsEditable && !string.IsNullOrWhiteSpace(col.SqlSource))
                        {
                            try
                            {
                                values[col.Key] = await _sql.ExecuteScalarAsync(col.SqlSource, regionId, year, key, col.RowKeyType);
                            }
                            catch (SqlSourceException ex)
                            {
                                vm.SqlWarnings.Add($"Колонка «{col.Header}»: запрос к данным основного приложения упал ({ex.InnerException?.Message}). Показан сохранённый снимок.");
                                values[col.Key] = snap.GetValueOrDefault(col.Key);
                            }
                        }
                        else
                        {
                            values[col.Key] = snap.TryGetValue(col.Key, out var sv) ? sv : manual.GetValueOrDefault(col.Key);
                        }
                        break;
                }
            }

            vm.Rows.Add(new RowViewModel { Key = key, Label = label, Values = values });
        }

        // Итоги по колонкам с total=sum
        foreach (var col in cfg.Columns.Where(c => c.Total == "sum"))
        {
            double s = 0; bool any = false;
            foreach (var r in vm.Rows)
                if (r.Values.TryGetValue(col.Key, out var v) && v.HasValue) { s += v.Value; any = true; }
            if (any) vm.Totals[col.Key] = Math.Round(s, 2);
        }

        await LoadErrorsIntoVm(vm);
        return vm;
    }

    /// <summary>Сохранить ручной ввод строки + пересчитать computed-snapshot + провалидировать.</summary>
    public async Task<(bool ok, List<ValidationError> errors)> SaveRowAsync(int reportId, string rowKey,
        Dictionary<string, double?> manualInput)
    {
        var inst = await _db.ReportInstances.Include(r => r.Rows).Include(r => r.ConfigVersion)
                     .FirstAsync(r => r.Id == reportId);
        var cfg = FormConfigService.Parse(inst.ConfigVersion!.ConfigJson);

        if (inst.Status != "draft") throw new InvalidOperationException("Отчёт уже отправлен/утверждён — редактирование запрещено.");
        if (inst.Year != CurrentYear) throw new InvalidOperationException($"Отчёт за {inst.Year} г. закрыт: данные за прошлые годы только для чтения.");

        var row = inst.Rows.FirstOrDefault(r => r.RowKey == rowKey) ?? EnsureRow(inst, rowKey, inst.Rows.Count);

        // 1. Валидация ручного ввода по атрибутам конфига (required/min/max)
        var errors = new List<ValidationError>();
        foreach (var col in cfg.Columns.Where(c => c.Kind is ColumnKind.Manual or ColumnKind.Dict))
        {
            manualInput.TryGetValue(col.Key, out var v);
            if (col.Required && v is null)
                errors.Add(new ValidationError { RowKey = rowKey, Message = $"Графа {col.Grafa} «{col.Header}»: обязательно к заполнению." });
            if (v is not null)
            {
                if (col.MinValue.HasValue && v < col.MinValue)
                    errors.Add(new ValidationError { RowKey = rowKey, Message = $"Графа {col.Grafa}: значение не может быть меньше {col.MinValue}." });
                if (col.MaxValue.HasValue && v > col.MaxValue)
                    errors.Add(new ValidationError { RowKey = rowKey, Message = $"Графа {col.Grafa}: значение не может быть больше {col.MaxValue}." });
            }
        }

        // 2. Обновляем хранилище
        row.ValuesJson = JsonSerializer.Serialize(
            manualInput.Where(kv => cfg.Columns.Any(c => c.Key == kv.Key && c.Kind is ColumnKind.Manual or ColumnKind.Dict))
                       .ToDictionary(kv => kv.Key, kv => kv.Value));

        // 3. Пересчитываем computed-колонки и пишем snapshot (текущий год, draft — можно)
        var snap = new Dictionary<string, double?>();
        foreach (var col in cfg.Columns.Where(c => c.Kind is ColumnKind.Computed or ColumnKind.Formula && !string.IsNullOrWhiteSpace(c.SqlSource)))
        {
            try { snap[col.Key] = await _sql.ExecuteScalarAsync(col.SqlSource!, inst.RegionId, inst.Year, rowKey, col.RowKeyType); }
            catch (SqlSourceException) { snap[col.Key] = ParseJson(row.ComputedSnapshotJson).GetValueOrDefault(col.Key); }
        }
        // dict-колонки: если пользователь не ввёл — берём из справочника
        foreach (var col in cfg.Columns.Where(c => c.Kind == ColumnKind.Dict && !string.IsNullOrWhiteSpace(c.DictSql)))
        {
            if (!manualInput.TryGetValue(col.Key, out var mv) || mv is null)
            {
                try { snap[col.Key] = await _sql.DictValueAsync(col.DictSql!, inst.RegionId, inst.Year); }
                catch (SqlSourceException) { }
            }
        }
        row.ComputedSnapshotJson = JsonSerializer.Serialize(snap);

        // 4. intra-правила по merged-значениям
        var merged = ValidationService.MergeValues(cfg, row);
        foreach (var kvp in manualInput) if (merged.ContainsKey(kvp.Key)) merged[kvp.Key] = kvp.Value;
        errors.AddRange(_val.ValidateRow(cfg, inst.Year >= 0 ? await _val.GetRulesAsync(cfg.FormKey, inst.Year) : new(), rowKey, merged));

        await _db.SaveChangesAsync();

        // 5. cross-правила по всему отчёту
        if (errors.All(e => e.Severity != "error"))
        {
            var allRows = (await _db.ReportRows.Where(r => r.ReportInstanceId == reportId).ToListAsync())
                .ToDictionary(r => r.RowKey, r => ValidationService.MergeValues(cfg, r));
            errors.AddRange(await _val.ValidateCrossAsync(inst, cfg, await _val.GetRulesAsync(cfg.FormKey, inst.Year), allRows));
        }

        return (errors.All(e => e.Severity != "error"), errors);
    }

    /// <summary>Финализация: заморозка snapshot'ов. После этого отчёт читается без обращения к SQL источников.</summary>
    public async Task FinalizeAsync(int reportId)
    {
        var inst = await _db.ReportInstances.Include(r => r.Rows).Include(r => r.ConfigVersion)
                     .FirstAsync(r => r.Id == reportId);
        if (inst.Status == "approved") throw new InvalidOperationException("Отчёт уже утверждён.");
        inst.Status = "approved";
        inst.FinalizedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
    }

    async Task LoadErrorsIntoVm(ReportViewModel vm)
    {
        var allRows = vm.Rows.ToDictionary(r => r.Key, r => r.Values);
        foreach (var r in vm.Rows)
            r.ColumnErrors = _val.ValidateRow(vm.Config, vm.Rules, r.Key, r.Values).Select(e => e.Message).ToList();
        vm.Errors = await _val.ValidateCrossAsync(vm.Instance, vm.Config, vm.Rules, allRows);
    }

    ReportRow EnsureRow(ReportInstance inst, string rowKey, int order)
    {
        var row = new ReportRow { ReportInstanceId = inst.Id, RowKey = rowKey, SortOrder = order };
        inst.Rows.Add(row);
        _db.ReportRows.Add(row);
        _db.SaveChanges();
        return row;
    }

    static Dictionary<string, double?> ParseJson(string json)
    {
        var d = new Dictionary<string, double?>();
        try
        {
            using var doc = JsonDocument.Parse(json);
            foreach (var p in doc.RootElement.EnumerateObject())
                d[p.Name] = p.Value.ValueKind == JsonValueKind.Null ? null : p.Value.GetDouble();
        }
        catch { }
        return d;
    }
}
