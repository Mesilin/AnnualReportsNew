using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using YasenReportsDemo.Data;
using YasenReportsDemo.Models;

namespace YasenReportsDemo.Services;

/// <summary>
/// Вычисление колонок kind=Computed по SQL из конфига.
/// Параметры @regionId, @year, @rowKey передаются как DbParameter — защитой от инъекции в демо пренебрегаем осознанно,
/// но запросы берутся из доверенного конфига (админ), а не от пользователя.
/// </summary>
public class SqlComputedService
{
    private readonly AppDbContext _db;
    public SqlComputedService(AppDbContext db) => _db = db;

    public async Task<double?> ExecuteScalarAsync(string sql, int regionId, int year, string? rowKey)
    {
        try
        {
            var conn = _db.Database.GetDbConnection();
            if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Add(P(cmd, "@regionId", regionId));
            cmd.Parameters.Add(P(cmd, "@year", year));
            cmd.Parameters.Add(P(cmd, "@rowKey", rowKey is null ? DBNull.Value : (object)rowKey));
            var res = await cmd.ExecuteScalarAsync();
            if (res is null || res is DBNull) return null;
            return Convert.ToDouble(res);
        }
        catch (Exception ex)
        {
            // Модель основного приложения могла измениться -> запрос упал.
            // В production здесь логируем и отдаём сохранённый snapshot (см. ReportService).
            Console.WriteLine($"[SqlComputed] Ошибка выполнения запроса: {ex.Message}");
            throw new SqlSourceException(sql, ex);
        }
    }

    public async Task<double?> DictValueAsync(string dictSql, int regionId, int year)
    {
        try
        {
            var conn = _db.Database.GetDbConnection();
            if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = dictSql;
            cmd.Parameters.Add(P(cmd, "@regionId", regionId));
            cmd.Parameters.Add(P(cmd, "@year", year));
            await using var rdr = await cmd.ExecuteReaderAsync();
            if (await rdr.ReadAsync())
            {
                var v = rdr.GetValue(1);
                return v is DBNull ? null : Convert.ToDouble(v);
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new SqlSourceException(dictSql, ex);
        }
    }

    static DbParameter P(DbCommand cmd, string name, object value)
    {
        var p = cmd.CreateParameter();
        p.ParameterName = name;
        p.Value = value;
        return p;
    }
}

public class SqlSourceException : Exception
{
    public string Sql { get; }
    public SqlSourceException(string sql, Exception inner) : base("SQL-источник из конфига формы выполнен с ошибкой", inner)
    {
        Sql = sql;
    }
}
