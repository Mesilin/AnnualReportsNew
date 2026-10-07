using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using NCalc;
using YasenReportsDemo.Data;
using YasenReportsDemo.Models;

namespace YasenReportsDemo.Services;

public class ValidationError
{
    public string RowKey { get; set; } = "";       // "" — для межформенных правил
    public string RuleDescription { get; set; } = "";
    public string Message { get; set; } = "";
    public string Severity { get; set; } = "error"; // error | warning
}

/// <summary>
/// Движок валидации. Правила хранятся в БД как выражения NCalc:
///  - intra: переменные [c1]..[cn] — значения колонок текущей строки;
///  - cross: переменные вида [form-2-avia.SUM_h3] — агрегаты по другой форме за тот же год/регион,
///           а [SUM_f4] — агрегат по текущей форме.
/// </summary>
public class ValidationService
{
    private readonly AppDbContext _db;
    public ValidationService(AppDbContext db) => _db = db;

    static readonly Regex CrossVar = new(@"\[(?<v>[A-Za-z0-9_\-]+\.[A-Za-z0-9_\-]+)\]", RegexOptions.Compiled);
    static readonly Regex LocalVar = new(@"\[(?<v>SUM_[A-Za-z0-9_\-]+|[A-Za-z0-9_\-]+)\]", RegexOptions.Compiled);

    public async Task<List<ValidationRule>> GetRulesAsync(string formKey, int year) =>
        await _db.ValidationRules
            .Where(r => r.Enabled && r.FormKey == formKey && r.EffectiveFromYear <= year)
            .OrderBy(r => r.SortOrder).ToListAsync();

    /// <summary>Проверить одну строку (intra-правила).</summary>
    public List<ValidationError> ValidateRow(FormConfig cfg, IReadOnlyList<ValidationRule> rules,
        string rowKey, Dictionary<string, double?> mergedValues)
    {
        var errors = new List<ValidationError>();
        foreach (var rule in rules.Where(r => r.Scope == "intra" && !string.IsNullOrWhiteSpace(r.Expression)))
        {
            try
            {
                var ok = EvaluateExpression(rule.Expression!, mergedValues, out _);
                if (!ok) errors.Add(new ValidationError
                {
                    RowKey = rowKey,
                    RuleDescription = rule.Description,
                    Message = rule.ErrorMessage,
                    Severity = rule.Severity
                });
            }
            catch (Exception ex)
            {
                errors.Add(new ValidationError
                {
                    RowKey = rowKey,
                    RuleDescription = rule.Description,
                    Message = $"Не удалось вычислить правило: {ex.Message}",
                    Severity = "warning"
                });
            }
        }
        return errors;
    }

    /// <summary>Межформенные правила: считаем агрегаты по всем строкам текущей и связанных форм.</summary>
    public async Task<List<ValidationError>> ValidateCrossAsync(ReportInstance inst, FormConfig cfg,
        IReadOnlyList<ValidationRule> rules, Dictionary<string, Dictionary<string, double?>> rowValuesMerged)
    {
        var errors = new List<ValidationError>();
        var crossRules = rules.Where(r => r.Scope == "cross" && !string.IsNullOrWhiteSpace(r.Expression)).ToList();
        if (crossRules.Count == 0) return errors;

        // Агрегаты текущей формы: SUM_<col>
        var currentAggregates = ComputeSums(cfg, rowValuesMerged);

        // Собираем все ссылки на другие формы из выражений: form-key.SUM_col
        var neededForms = new HashSet<string>();
        foreach (var r in crossRules)
            foreach (Match mt in CrossVar.Matches(r.Expression!))
                neededForms.Add(mt.Groups["v"].Value.Split('.')[0]);
        neededForms.Remove(cfg.FormKey);

        // Загружаем связанные отчёты того же года/региона
        var related = new Dictionary<string, Dictionary<string, double?>?>();
        foreach (var fk in neededForms)
        {
            var other = await _db.ReportInstances
                .Include(x => x.Rows)
                .Include(x => x.ConfigVersion)
                .FirstOrDefaultAsync(x => x.FormKey == fk && x.Year == inst.Year && x.RegionId == inst.RegionId);
            if (other is null) { related[fk] = null; continue; }
            var otherCfg = FormConfigService.Parse(other.ConfigVersion!.ConfigJson);
            var vals = other.Rows.ToDictionary(row => row.RowKey,
                row => MergeValues(otherCfg, row));
            related[fk] = ComputeSums(otherCfg, vals);
        }

        foreach (var rule in crossRules)
        {
            var expr = rule.Expression!;
            try
            {
                var parameters = new Dictionary<string, object>();
                foreach (var (k, v) in currentAggregates) if (v.HasValue) parameters[k] = v.Value;
                bool missing = false;
                foreach (Match mt in CrossVar.Matches(expr))
                {
                    var full = mt.Groups["v"].Value; // "form-2-avia.SUM_h3"
                    var parts = full.Split('.', 2);
                    if (!related.TryGetValue(parts[0], out var aggMap) || aggMap is null)
                    {
                        // связанная форма не заполнена — не блокируем сохранение, предупреждаем
                        errors.Add(new ValidationError
                        {
                            RuleDescription = rule.Description,
                            Message = $"Межформенный контроль пропущен: отчёт «{parts[0]}» за {inst.Year} г. ещё не создан.",
                            Severity = "warning"
                        });
                        missing = true;
                        break;
                    }
                    if (!aggMap.TryGetValue(parts[1], out var val)) { missing = true; break; }
                    parameters[full] = val ?? 0d;
                }
                if (missing) continue;

                var ok = EvaluateExpressionRaw(expr, parameters, out _);
                if (!ok) errors.Add(new ValidationError
                {
                    RuleDescription = rule.Description,
                    Message = rule.ErrorMessage,
                    Severity = rule.Severity
                });
            }
            catch (Exception ex)
            {
                errors.Add(new ValidationError
                {
                    RuleDescription = rule.Description,
                    Message = $"Ошибка вычисления межформенного правила: {ex.Message}",
                    Severity = "warning"
                });
            }
        }
        return errors;
    }

    /// <summary>Слияние ручных значений + snapshot/computed для проверки правил всей строкой.</summary>
    public static Dictionary<string, double?> MergeValues(FormConfig cfg, ReportRow row)
    {
        var manual = ParseValues(row.ValuesJson);
        var snap = ParseValues(row.ComputedSnapshotJson);
        var merged = new Dictionary<string, double?>();
        foreach (var col in cfg.Columns)
        {
            if (col.Kind is ColumnKind.Manual or ColumnKind.Dict)
                merged[col.Key] = manual.TryGetValue(col.Key, out var v) ? v : snap.GetValueOrDefault(col.Key);
            else
                merged[col.Key] = snap.TryGetValue(col.Key, out var sv) ? sv : manual.GetValueOrDefault(col.Key);
        }
        return merged;
    }

    static Dictionary<string, double?> ParseValues(string json)
    {
        var dict = new Dictionary<string, double?>();
        try
        {
            using var doc = System.Text.Json.JsonDocument.Parse(json);
            foreach (var p in doc.RootElement.EnumerateObject())
                dict[p.Name] = p.Value.ValueKind == System.Text.Json.JsonValueKind.Null ? null : p.Value.GetDouble();
        }
        catch { }
        return dict;
    }

    static Dictionary<string, double?> ComputeSums(FormConfig cfg, Dictionary<string, Dictionary<string, double?>> rows)
    {
        var sums = new Dictionary<string, double?>();
        foreach (var col in cfg.Columns)
        {
            double s = 0; bool any = false;
            foreach (var (_, vals) in rows)
                if (vals.TryGetValue(col.Key, out var v) && v.HasValue) { s += v.Value; any = true; }
            sums["SUM_" + col.Key] = any ? s : null;
        }
        return sums;
    }

    // ===== NCalc helpers =====

    static bool EvaluateExpression(string expr, Dictionary<string, double?> vars, out double? resultNum)
    {
        var mapped = new Dictionary<string, object>();
        foreach (var (k, v) in vars) mapped[k] = v ?? 0d;
        return EvaluateExpressionRaw(expr, mapped, out resultNum);
    }

    static bool EvaluateExpressionRaw(string expr, Dictionary<string, object> parameters, out double? resultNum)
    {
        resultNum = null;
        // [name] -> name (NCalc использует [x] для параметров — оставляем как есть, он сам понимает скобки)
        var e = new Expression(expr, CultureInfo.InvariantCulture);
        foreach (var (k, v) in parameters)
            e.Parameters[k] = Convert.ToDouble(v, CultureInfo.InvariantCulture);
        var res = e.Evaluate();
        if (res is bool b) return b;
        if (res is not null) { resultNum = Convert.ToDouble(res, CultureInfo.InvariantCulture); }
        throw new InvalidOperationException("Выражение правила должно возвращать boolean");
    }
}
