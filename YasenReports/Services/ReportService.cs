using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using YasenReports.Api.Data;
using YasenReports.Api.Models;

namespace YasenReports.Api.Services;

/// <summary>
/// Сервис построения «каркаса» отчёта по конфигурации формы и проверки правилами валидации.
/// </summary>
public class ReportService
{
    private readonly IDbContextFactory<YasenDbContext> _dbFactory;
    private readonly SqlColumnExecutor _sqlExecutor;

    public ReportService(IDbContextFactory<YasenDbContext> dbFactory, SqlColumnExecutor sqlExecutor)
    {
        _dbFactory = dbFactory;
        _sqlExecutor = sqlExecutor;
    }

    public async Task<ReportFormConfig?> GetActiveConfigAsync(string formCode, int year, CancellationToken ct = default)
    {
        using var db = await _dbFactory.CreateDbContextAsync(ct);
        return await db.FormConfigs.FirstOrDefaultAsync(
            x => x.Code == formCode && x.Year == year && x.IsActive, ct);
    }

    /// <summary>
    /// Собрать пустую (или частично заполненную из сохранённого снимка) форму за год:
    /// строки по субъектам + сводная, значения SQL-колонок — из запросов/снимков,
    /// Manual-колонки — из ранее сохранённых данных, если они есть.
    /// </summary>
    public async Task<List<FormRowData>> BuildRowsAsync(ReportFormConfig cfg, bool includeSaved, CancellationToken ct = default)
    {
        var def = FormConfigSerializer.Deserialize(cfg.Config);
        using var db = await _dbFactory.CreateDbContextAsync(ct);

        var subjects = await db.Subjects.Where(s => s.IsActive)
            .OrderBy(s => s.Sorting ?? 0).ThenBy(s => s.Name) // сортировка справочника
            .Select(s => new { s.Id, s.Code, s.Name, s.ForestFundAreaThousandHa })
            .ToListAsync(ct);

        // Уже сохранённые данные (ручной ввод) — по субъекту+строке
        var saved = new Dictionary<(string, Guid?), ReportInstance>();
        if (includeSaved)
        {
            var instances = await db.Reports
                .Where(r => r.FormCode == cfg.Code && r.Year == cfg.Year)
                .ToListAsync(ct);
            foreach (var r in instances)
                saved[(r.RowKey, r.SubjectId)] = r;
        }

        var rows = new List<FormRowData>();
        foreach (var rowDef in def.Rows)
        {
            if (rowDef.SubjectScoped)
            {
                foreach (var subj in subjects)
                    rows.Add(await BuildRowAsync(def, rowDef, cfg, subj.Id, subj.Name, subj.Code, subj.ForestFundAreaThousandHa, saved, ct));
            }
            else
            {
                rows.Add(await BuildRowAsync(def, rowDef, cfg, null, rowDef.Label, null, null, saved, ct));
            }
        }

        // Сводные строки агрегируются из субъектных
        foreach (var rowDef in def.Rows.Where(r => !r.SubjectScoped && r.Aggregate == "sum"))
        {
            var total = rows.First(x => x.RowKey == rowDef.Key);
            foreach (var col in def.Columns.Where(c => c.Numeric))
            {
                var sum = rows.Where(x => x.RowKey != rowDef.Key)
                    .Select(x => x.Cells.TryGetValue(col.Code, out var v) ? v.Number : null)
                    .Where(v => v.HasValue).Sum(v => v!.Value);
                if (rows.Any(x => x.RowKey != rowDef.Key && x.Cells.ContainsKey(col.Code)))
                    total.Cells[col.Code] = new CellValue { Number = Math.Round(sum, 2) };
            }
        }

        return rows;
    }

    private async Task<FormRowData> BuildRowAsync(FormDefinition def, RowDefinition rowDef, ReportFormConfig cfg,
        Guid? subjectId, string name, string? code, decimal? forestFundArea,
        Dictionary<(string, Guid?), ReportInstance> saved, CancellationToken ct)
    {
        var row = new FormRowData { RowKey = rowDef.Key, SubjectId = subjectId };

        // Сохранённый ручной ввод
        Dictionary<string, JsonElement>? savedCells = null;
        if (subjectId is not null && saved.TryGetValue((rowDef.Key, subjectId), out var inst) ||
            subjectId is null && saved.TryGetValue((rowDef.Key, null), out inst))
        {
            savedCells = inst.Data.TryDeserializeToDictionary();
        }

        foreach (var col in def.Columns)
        {
            switch (col.SourceType)
            {
                case ColumnSourceType.Text:
                    var textVal = col.Code == "name" ? name : col.Code == "code" ? code : null;
                    row.Cells[col.Code] = new CellValue { Text = textVal };
                    break;

                case ColumnSourceType.SqlQuery when subjectId is not null && col.Expression is not null:
                    var val = await _sqlExecutor.GetColumnValueAsync(cfg.Code, col.Code, col.Expression, cfg.Year, subjectId.Value, ct);
                    row.Cells[col.Code] = new CellValue { Number = val };
                    break;

                case ColumnSourceType.Manual:
                    // приоритет: сохранённое значение > значение из справочника > default
                    if (savedCells is not null && savedCells.TryGetValue(col.Code, out var sv) &&
                        sv.ValueKind == JsonValueKind.Number)
                        row.Cells[col.Code] = new CellValue { Number = sv.GetDecimal() };
                    else if (col.DefaultFromCatalog == "subject.forestFundArea" && forestFundArea.HasValue)
                        row.Cells[col.Code] = new CellValue { Number = forestFundArea.Value };
                    else if (col.DefaultValue.HasValue)
                        row.Cells[col.Code] = new CellValue { Number = col.DefaultValue.Value };
                    else
                        row.Cells[col.Code] = new CellValue { Number = null };
                    break;

                default:
                    row.Cells[col.Code] = new CellValue { Number = null };
                    break;
            }
        }

        EvaluateComputedColumns(def, row);
        return row;
    }

    /// <summary>Вычислить Computed-колонки ("col9+col11") после заполнения остальных.</summary>
    public static void EvaluateComputedColumns(FormDefinition def, FormRowData row)
    {
        foreach (var col in def.Columns.Where(c => c.SourceType == ColumnSourceType.Computed && c.Expression is not null))
        {
            if (col.Expression == "$rownum") continue;
            decimal sum = 0;
            foreach (var t in FormulaParser.Parse(col.Expression))
            {
                var v = row.Cells.TryGetValue(t.ColumnCode, out var cell) ? cell.Number : null;
                sum += (v ?? 0) * t.Sign;
            }
            row.Cells[col.Code] = new CellValue { Number = Math.Round(sum, 2) };
        }
    }

    // ------------------------------------------------------------------
    // Валидация
    // ------------------------------------------------------------------

    /// <summary>Проверить набор строк всеми активными правилами формы за год.</summary>
    public async Task<List<ValidationIssue>> ValidateAsync(string formCode, int year, IReadOnlyList<FormRowData> rows, CancellationToken ct = default)
    {
        using var db = await _dbFactory.CreateDbContextAsync(ct);
        var cfgEntity = await GetActiveConfigInternal(db, formCode, year, ct);
        if (cfgEntity is null)
            return new List<ValidationIssue> { new(0, $"Конфигурация формы {formCode} за {year} год не найдена", true, "Ошибка конфигурации") };

        var def = FormConfigSerializer.Deserialize(cfgEntity.Config);
        var rules = await db.ValidationRules
            .Where(r => r.Year == year && r.IsActive && r.FormCode == formCode)
            .ToListAsync(ct);

        var issues = new List<ValidationIssue>();

        // Для межформенных правил нужны данные второй формы (сохранённые снимки)
        var otherFormDataCache = new Dictionary<string, List<FormRowData>>();

        foreach (var rule in rules)
        {
            if (rule.Kind == ValidationRuleKind.IntraForm)
            {
                foreach (var row in rows)
                {
                    var left = EvalFormula(rule.LeftExpression, row);
                    var right = EvalFormula(rule.RightExpression, row);
                    AddIfViolated(issues, rule, row, left, right);
                }
            }
            else // InterForm
            {
                var otherForm = rule.RightFormCode
                    ?? throw new InvalidOperationException($"В правиле {rule.Id} не указан RightFormCode");

                if (!otherFormDataCache.TryGetValue(otherForm, out var otherRows))
                {
                    var otherCfg = await GetActiveConfigInternal(db, otherForm, year, ct);
                    otherRows = otherCfg is null
                        ? new List<FormRowData>()
                        : await BuildRowsAsync(otherCfg, includeSaved: true, ct);
                    otherFormDataCache[otherForm] = otherRows;
                }

                foreach (var row in rows)
                {
                    if (row.SubjectId is null) continue;
                    var otherRow = otherRows.FirstOrDefault(r => r.SubjectId == row.SubjectId && r.RowKey == row.RowKey);
                    if (otherRow is null) continue;

                    var left = EvalFormula(rule.LeftExpression, row);
                    var right = EvalFormulaCrossForm(rule.RightExpression, otherRow);
                    AddIfViolated(issues, rule, row, left, right);
                }
            }
        }

        // Проверка обязательности и диапазонов ручного ввода
        foreach (var row in rows)
        foreach (var col in def.Columns.Where(c => c.SourceType == ColumnSourceType.Manual))
        {
            var v = row.Cells.TryGetValue(col.Code, out var cell) ? cell.Number : null;
            if (col.Required && row.RowKey == "subject" && v is null)
                issues.Add(new ValidationIssue(0, $"{col.Caption}: обязательно к заполнению", true,
                    $"Пропущено обязательное значение (графа {col.Number})"));
            if (v.HasValue && ((col.Min.HasValue && v < col.Min) || (col.Max.HasValue && v > col.Max)))
                issues.Add(new ValidationIssue(0, $"{col.Caption}: значение вне допустимого диапазона", true,
                    $"Значение {v} вне диапазона [{col.Min}, {col.Max}]"));
        }

        return issues;
    }

    private static void AddIfViolated(List<ValidationIssue> issues, ValidationRuleConfig rule, FormRowData row,
        decimal? left, decimal? right)
    {
        if (left is null || right is null) return; // нет данных — не проверяем
        var ok = rule.Operator switch
        {
            ComparisonOperator.LessOrEqual => left <= right + rule.Tolerance,
            ComparisonOperator.GreaterOrEqual => left >= right - rule.Tolerance,
            ComparisonOperator.Equal => Math.Abs(left.Value - right.Value) <= rule.Tolerance,
            ComparisonOperator.Less => left < right,
            ComparisonOperator.Greater => left > right,
            _ => true,
        };
        if (!ok)
        {
            var who = row.SubjectId is null ? row.RowKey : "субъект";
            issues.Add(new ValidationIssue(rule.Id, rule.Description, rule.IsBlocking,
                $"Нарушение ({who}): {rule.LeftExpression} = {left} vs {rule.RightExpression} = {right}"));
        }
    }

    private static decimal? EvalFormula(string? expression, FormRowData row)
    {
        if (string.IsNullOrWhiteSpace(expression)) return null;
        decimal sum = 0; bool any = false;
        foreach (var t in FormulaParser.Parse(expression))
        {
            if (t.FormCode is not null) continue; // для межформенных — EvalFormulaCrossForm
            var v = row.Cells.TryGetValue(t.ColumnCode, out var cell) ? cell.Number : null;
            if (v is null) return null;
            sum += v.Value * t.Sign;
            any = true;
        }
        return any ? sum : null;
    }

    /// <summary>Формула из другой формы: префикс "form.col" или без префикса — колонка той (другой) формы.</summary>
    private static decimal? EvalFormulaCrossForm(string? expression, FormRowData otherRow)
    {
        if (string.IsNullOrWhiteSpace(expression)) return null;
        decimal sum = 0; bool any = false;
        foreach (var t in FormulaParser.Parse(expression))
        {
            var v = otherRow.Cells.TryGetValue(t.ColumnCode, out var cell) ? cell.Number : null;
            if (v is null) return null;
            sum += v.Value * t.Sign;
            any = true;
        }
        return any ? sum : null;
    }

    private static Task<ReportFormConfig?> GetActiveConfigInternal(YasenDbContext db, string formCode, int year, CancellationToken ct) =>
        db.FormConfigs.FirstOrDefaultAsync(x => x.Code == formCode && x.Year == year && x.IsActive, ct);
}

public static class JsonElementExt
{
    public static Dictionary<string, JsonElement>? TryDeserializeToDictionary(this System.Text.Json.JsonElement el)
    {
        try
        {
            if (el.ValueKind != System.Text.Json.JsonValueKind.Object) return null;
            return el.EnumerateObject().ToDictionary(p => p.Name, p => p.Value.Clone());
        }
        catch { return null; }
    }
}
