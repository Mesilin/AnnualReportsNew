using Microsoft.EntityFrameworkCore;
using YasenReports.Api.Data;
using YasenReports.Api.Models;

namespace YasenReports.Api.Services;

/// <summary>
/// Управление версиями конфигураций форм и правил валидации.
/// Новый год = новая строка ReportFormConfig с обновлённым JSON — без миграций БД.
/// </summary>
public class ConfigService
{
    private readonly IDbContextFactory<YasenDbContext> _dbFactory;

    public ConfigService(IDbContextFactory<YasenDbContext> dbFactory) => _dbFactory = dbFactory;

    public async Task<List<ReportFormConfig>> ListFormsAsync(int? year = null, CancellationToken ct = default)
    {
        using var db = await _dbFactory.CreateDbContextAsync(ct);
        var q = db.FormConfigs.AsQueryable();
        if (year is not null) q = q.Where(x => x.Year == year);
        return await q.OrderBy(x => x.Code).ThenByDescending(x => x.Year).ToListAsync(ct);
    }

    public async Task<ReportFormConfig?> GetFormAsync(string code, int year, CancellationToken ct = default)
    {
        using var db = await _dbFactory.CreateDbContextAsync(ct);
        return await db.FormConfigs.FirstOrDefaultAsync(x => x.Code == code && x.Year == year, ct);
    }

    /// <summary>Создать/обновить конфигурацию формы (проверка корректности JSON).</summary>
    public async Task<ReportFormConfig> UpsertFormAsync(ReportFormConfig input, CancellationToken ct = default)
    {
        // Валидация структуры конфига до сохранения
        var def = FormConfigSerializer.Deserialize(input.Config);
        if (def.Columns.Count == 0) throw new ArgumentException("В конфигурации формы нет колонок");
        if (string.IsNullOrWhiteSpace(def.Code)) def.Code = input.Code;

        using var db = await _dbFactory.CreateDbContextAsync(ct);
        var existing = await db.FormConfigs.FirstOrDefaultAsync(
            x => x.Code == input.Code && x.Year == input.Year, ct);

        if (existing is null)
        {
            input.Id = 0;
            input.CreatedUtc = DateTime.UtcNow;
            db.FormConfigs.Add(input);
            await db.SaveChangesAsync(ct);
            return input;
        }

        existing.Config = input.Config;
        existing.Title = input.Title;
        existing.IsActive = input.IsActive;
        await db.SaveChangesAsync(ct);
        return existing;
    }

    /// <summary>
    /// Клонировать конфигурацию формы на новый год (точка входа для ежегодной адаптации:
    /// скопировали, поменяли заголовки/колобки в JSON — и всё работает без пересборки приложения).
    /// </summary>
    public async Task<ReportFormConfig> CloneFormToYearAsync(string code, int fromYear, int toYear, CancellationToken ct = default)
    {
        using var db = await _dbFactory.CreateDbContextAsync(ct);
        var src = await db.FormConfigs.FirstOrDefaultAsync(x => x.Code == code && x.Year == fromYear, ct)
                  ?? throw new InvalidOperationException($"Форма {code} за {fromYear} не найдена");
        if (await db.FormConfigs.AnyAsync(x => x.Code == code && x.Year == toYear, ct))
            throw new InvalidOperationException($"Форма {code} за {toYear} уже существует");

        var clone = new ReportFormConfig
        {
            Code = src.Code,
            Year = toYear,
            Title = src.Title.Replace(fromYear.ToString(), toYear.ToString()),
            Config = src.Config,
            IsActive = true,
        };
        db.FormConfigs.Add(clone);
        await db.SaveChangesAsync(ct);
        return clone;
    }

    public async Task<List<ValidationRuleConfig>> ListRulesAsync(int? year = null, string? formCode = null, CancellationToken ct = default)
    {
        using var db = await _dbFactory.CreateDbContextAsync(ct);
        var q = db.ValidationRules.AsQueryable();
        if (year is not null) q = q.Where(r => r.Year == year);
        if (!string.IsNullOrEmpty(formCode)) q = q.Where(r => r.FormCode == formCode);
        return await q.OrderBy(r => r.FormCode).ThenBy(r => r.Id).ToListAsync(ct);
    }

    public async Task<ValidationRuleConfig> UpsertRuleAsync(ValidationRuleConfig rule, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(rule.LeftExpression) || string.IsNullOrWhiteSpace(rule.RightExpression))
            throw new ArgumentException("У правила должны быть заданы левая и правая части");

        using var db = await _dbFactory.CreateDbContextAsync(ct);
        if (rule.Id == 0)
        {
            db.ValidationRules.Add(rule);
        }
        else
        {
            var existing = await db.ValidationRules.FindAsync(new object[] { rule.Id }, ct)
                           ?? throw new InvalidOperationException("Правило не найдено");
            db.Entry(existing).CurrentValues.SetValues(rule);
        }
        await db.SaveChangesAsync(ct);
        return rule;
    }

    public async Task DeleteRuleAsync(int id, CancellationToken ct = default)
    {
        using var db = await _dbFactory.CreateDbContextAsync(ct);
        var rule = await db.ValidationRules.FindAsync(new object[] { id }, ct);
        if (rule is not null)
        {
            rule.IsActive = false; // мягкое удаление — правила остаются в истории годов
            await db.SaveChangesAsync(ct);
        }
    }
}
