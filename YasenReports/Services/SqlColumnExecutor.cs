using System.Data.Common;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using YasenReports.Api.Data;
using YasenReports.Api.Models;

namespace YasenReports.Api.Services;

/// <summary>Одна ошибка/замечание проверки.</summary>
public record ValidationIssue(int RuleId, string Description, bool IsBlocking, string Message);

/// <summary>Значение ячейки строки отчёта.</summary>
public class CellValue
{
    public decimal? Number { get; set; }
    public string? Text { get; set; }
}

/// <summary>Заполненная строка формы: RowKey + SubjectId + значения колонок.</summary>
public class FormRowData
{
    public string RowKey { get; set; } = "subject";
    public Guid? SubjectId { get; set; }
    public Dictionary<string, CellValue> Cells { get; set; } = new();
}

/// <summary>
/// Сервис исполнения SQL-колонок из конфигурации формы.
/// Запросы берутся из JSON-конфига и выполняются против БД основного приложения
/// (строка подключения "AvialesConnection"; если не настроена — используется локальная YasenReports).
/// Результаты за год кэшируются в SourceDataSnapshot, чтобы прошлые отчёты можно было
/// читать без повторного выполнения запросов (модели основного приложения могут измениться).
/// </summary>
public class SqlColumnExecutor
{
    private readonly IDbContextFactory<YasenDbContext> _dbFactory;
    private readonly IConfiguration _config;

    public SqlColumnExecutor(IDbContextFactory<YasenDbContext> dbFactory, IConfiguration config)
    {
        _dbFactory = dbFactory;
        _config = config;
    }

    private static readonly Regex SafeGuid = new(@"^[0-9a-fA-F-]{36}$", RegexOptions.Compiled);
    private static readonly Regex SafeYear = new(@"^\d{4}$", RegexOptions.Compiled);

    /// <summary>Подстановка плейсхолдеров с валидацией значений (защита от инъекций).</summary>
    public static string Bind(string sqlTemplate, int year, Guid subjectId)
    {
        if (!SafeYear.IsMatch(year.ToString())) throw new ArgumentException("Некорректный год");
        if (!SafeGuid.IsMatch(subjectId.ToString())) throw new ArgumentException("Некорректный subjectId");
        return sqlTemplate.Replace("{year}", year.ToString()).Replace("{subjectId}", subjectId.ToString());
    }

    /// <summary>Можно ли перегенерировать данные за год (только текущий или будущий год).</summary>
    public static bool CanRegenerate(int year) => year >= DateTime.UtcNow.Year;

    public async Task<decimal?> ExecuteAsync(string sqlTemplate, int year, Guid subjectId, CancellationToken ct = default)
    {
        var sql = Bind(sqlTemplate, year, subjectId);
        var connStr = _config.GetConnectionString("AvialesConnection")
                      ?? _config.GetConnectionString("DefaultConnection");

        await using var conn = CreateConnection(connStr);
        await conn.OpenAsync(ct);
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.CommandTimeout = 60;

        try
        {
            var result = await cmd.ExecuteScalarAsync(ct);
            if (result is null or DBNull) return null;
            return Convert.ToDecimal(result);
        }
        catch (DbException)
        {
            // Модель основного приложения могла измениться — запрос падает.
            // Это не фатально: отдаём ранее сохранённый снимок (см. GetColumnValueAsync).
            return null;
        }
    }

    private static DbConnection CreateConnection(string connectionString)
    {
        var factory = Npgsql.NpgsqlFactory.Instance;
        var conn = factory.CreateConnection();
        conn!.ConnectionString = connectionString;
        return conn;
    }

    /// <summary>
    /// Получить значение SQL-колонки: для текущего года — выполнить запрос и обновить снимок;
    /// для прошлых лет — прочитать сохранённый снимок (без перегенерации).
    /// </summary>
    public async Task<decimal?> GetColumnValueAsync(string formCode, string columnCode, string sqlTemplate,
        int year, Guid subjectId, CancellationToken ct = default)
    {
        var key = $"{formCode}.{columnCode}";
        using var db = await _dbFactory.CreateDbContextAsync(ct);

        decimal? value = null;
        if (CanRegenerate(year))
        {
            value = await ExecuteAsync(sqlTemplate, year, subjectId, ct);
            if (value.HasValue)
                await UpsertSnapshotAsync(db, year, subjectId, key, value.Value, ct);
        }

        if (!value.HasValue)
        {
            var snap = await db.SourceSnapshots.FirstOrDefaultAsync(
                s => s.Year == year && s.SubjectId == subjectId && s.ColumnKey == key, ct);
            value = snap?.Value;
        }
        return value;
    }

    private static async Task UpsertSnapshotAsync(YasenDbContext db, int year, Guid subjectId,
        string key, decimal value, CancellationToken ct)
    {
        var snap = await db.SourceSnapshots.FirstOrDefaultAsync(
            s => s.Year == year && s.SubjectId == subjectId && s.ColumnKey == key, ct);
        if (snap is null)
            db.SourceSnapshots.Add(new SourceDataSnapshot { Year = year, SubjectId = subjectId, ColumnKey = key, Value = value });
        else
        {
            snap.Value = value;
            snap.CapturedUtc = DateTime.UtcNow;
        }
        await db.SaveChangesAsync(ct);
    }
}
