using System.Data.Common;
using System.Globalization;
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

    public async Task<double?> ExecuteScalarAsync(string sql, int regionId, int year, string? rowKey, string? rowKeyType = null)
    {
        try
        {
            var conn = _db.Database.GetDbConnection();
            if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Add(P(cmd, "@regionId", regionId));
            cmd.Parameters.Add(P(cmd, "@year", year));
            cmd.Parameters.Add(PRowKey(cmd, rowKey, rowKeyType));
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

    /// <summary>
    /// Параметр @rowKey с типом из конфига колонки (rowKeyType). По умолчанию строка (text),
    /// но если SQL сравнивает rowKey с числовым столбцом (integer/bigint), нужно явно
    /// передать числовое значение — иначе Npgsql отправит text и PostgreSQL не найдёт
    /// оператор сравнения (SQLSTATE 42883: «оператор не существует: integer = text»).
    /// </summary>
    static DbParameter PRowKey(DbCommand cmd, string? rowKey, string? rowKeyType)
    {
        if (rowKey is null) return P(cmd, "@rowKey", DBNull.Value);

        object value = NormalizeRowKey(rowKey, rowKeyType);
        return P(cmd, "@rowKey", value);
    }

    static object NormalizeRowKey(string rowKey, string? rowKeyType)
    {
        var type = rowKeyType?.Trim().ToLowerInvariant();
        switch (type)
        {
            // Integer-колонки (напр. FlightHours.AirDivisionId): отправляем int32,
            // иначе Npgsql выведет bigint и PostgreSQL не найдёт оператор integer = bigint.
            case "int":
            case "integer":
                return int.TryParse(rowKey, out var i) ? i : rowKey;
            case "bigint":
            case "long":
                return long.TryParse(rowKey, out var l) ? l : rowKey;
            // Decimal-колонки (напр. numeric/decimal в основном приложении): отправляем decimal,
            // иначе Npgsql отправит text и PostgreSQL не найдёт оператор numeric = text (42883).
            // Инвариантная культура: конфиг может содержать точку как десятичный разделитель.
            case "decimal":
            case "numeric":
            case "money":
                return decimal.TryParse(rowKey, NumberStyles.Number, CultureInfo.InvariantCulture, out var dec)
                    ? dec
                    : rowKey;
            case "double":
            case "float":
            case "real":
                return double.TryParse(rowKey, NumberStyles.Float, CultureInfo.InvariantCulture, out var dbl)
                    ? dbl
                    : rowKey;
            case "uuid":
                return Guid.TryParse(rowKey, out var g) ? g : rowKey;
            default:
                // text/varchar/null — строка, как раньше.
                return rowKey;
        }
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
