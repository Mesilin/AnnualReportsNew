using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using YasenReportsDemo.Data;
using YasenReportsDemo.Models;

namespace YasenReportsDemo.Services;

/// <summary>Загрузка и парсинг JSON-конфигураций форм с учётом года отчёта.</summary>
public class FormConfigService
{
    private readonly AppDbContext _db;
    public FormConfigService(AppDbContext db) => _db = db;

    static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true
    };

    public static FormConfig Parse(string json) =>
        JsonSerializer.Deserialize<FormConfig>(json, JsonOpts)!;

    /// <summary>Активная версия конфигурации формы на указанный год: максимальная версия с EffectiveFromYear &lt;= year.</summary>
    public async Task<FormConfigVersion?> GetActiveForYearAsync(string formKey, int year)
    {
        return await _db.FormConfigs
            .Where(c => c.FormKey == formKey && c.IsActive && c.EffectiveFromYear <= year)
            .OrderByDescending(c => c.EffectiveFromYear).ThenByDescending(c => c.Version)
            .FirstOrDefaultAsync();
    }

    public async Task<List<FormConfigVersion>> ListAllActiveAsync() =>
        await _db.FormConfigs.Where(c => c.IsActive)
            .OrderBy(c => c.FormKey).ThenByDescending(c => c.Version)
            .ToListAsync();

    /// <summary>Список строк формы (из справочников основного приложения) по rowSource.</summary>
    public async Task<List<RowKeyLabel>> GetRowKeysAsync(string rowSource, int regionId)
    {
        switch (rowSource)
        {
            case "airDivision":
                return await _db.AirDivisions.Where(a => a.RegionId == regionId)
                    .OrderBy(a => a.Code)
                    .Select(a => new RowKeyLabel { Key = a.Id.ToString(), Label = a.Name + " (код " + a.Code + ")" })
                    .ToListAsync();
            case "forestry":
                return await _db.Forestries.Where(f => f.RegionId == regionId)
                    .OrderBy(f => f.Name)
                    .Select(f => new RowKeyLabel { Key = f.Id.ToString(), Label = f.Name })
                    .ToListAsync();
            case "region":
                return await _db.Regions.OrderBy(r => r.Name)
                    .Select(r => new RowKeyLabel { Key = r.Id.ToString(), Label = r.Name })
                    .ToListAsync();
            default:
                return new();
        }
    }
}

public class RowKeyLabel
{
    public string Key { get; set; } = "";
    public string Label { get; set; } = "";
    public void Deconstruct(out string key, out string label) { key = Key; label = Label; }
}
