using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace YasenReports.Api.Models;

/// <summary>
/// Версия конфигурации формы отчёта (например «Форма 1-авиа» за 2025 год).
/// Собственно структура формы хранится в JSON-поле <see cref="ConfigJson"/> —
/// это позволяет менять состав/заголовки колонок без миграций БД.
/// </summary>
public class ReportFormConfig
{
    public int Id { get; set; }

    /// <summary>Машинный код формы, неизменный между годами: "1-avia", "2-1-avia".</summary>
    [MaxLength(64)]
    public string Code { get; set; } = "";

    /// <summary>Год, к которому относится конфигурация формы.</summary>
    public int Year { get; set; }

    [MaxLength(256)]
    public string Title { get; set; } = "";

    /// <summary>JSON описания формы (строки, колонки, источники данных). См. FormDefinition.</summary>
    public JsonElement Config { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Правило валидации. Хранится как декларативное выражение: левая часть, операция, правая часть.
/// Части задаются формулами над колонками форм, например "col8" или "col3+col4+col5",
/// для межформенных правил используется префикс формы: "2-1-avia.col1 &lt;= 1-avia.col12".
/// </summary>
public class ValidationRuleConfig
{
    public int Id { get; set; }

    public int Year { get; set; }

    /// <summary>Код формы, для которой проверяется правило (для межформенных — «главная» форма).</summary>
    [MaxLength(64)]
    public string FormCode { get; set; } = "";

    [MaxLength(512)]
    public string Description { get; set; } = "";

    public ValidationRuleKind Kind { get; set; } = ValidationRuleKind.IntraForm;

    /// <summary>Формула левой части, напр. "col8" или "2-1-avia.col1". Пусто — берётся LeftColumnCode.</summary>
    [MaxLength(256)]
    public string? LeftExpression { get; set; }

    /// <summary>Формула правой части, напр. "col3+col4+col5" или "1-avia.col1".</summary>
    [MaxLength(256)]
    public string? RightExpression { get; set; }

    /// <summary>Код второй формы для межформенного правила.</summary>
    [MaxLength(64)]
    public string? RightFormCode { get; set; }

    public ComparisonOperator Operator { get; set; } = ComparisonOperator.LessOrEqual;

    /// <summary>Допустимое отклонение при проверке равенства (для операторов Equal), тыс. га / часов.</summary>
    public decimal Tolerance { get; set; } = 0.01m;

    /// <summary>Правило обязательное (нарушение блокирует публикацию) или предупреждение.</summary>
    public bool IsBlocking { get; set; } = true;

    public bool IsActive { get; set; } = true;
}
