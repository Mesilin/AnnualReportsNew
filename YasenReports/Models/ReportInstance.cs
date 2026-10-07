using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace YasenReports.Api.Models;

/// <summary>
/// Сохранённый экземпляр отчёта (заполненная форма за год по субъекту/сводная).
/// Данные хранятся в JSON — «замороженный» снимок, читаемый без перегенерации.
/// </summary>
public class ReportInstance
{
    public Guid Id { get; set; }

    [MaxLength(64)]
    public string FormCode { get; set; } = "";

    public int Year { get; set; }

    /// <summary>Субъект, к которому относятся данные (для сводных строк может быть null).</summary>
    public Guid? SubjectId { get; set; }
    public Subject? Subject { get; set; }

    /// <summary>Ключ строки формы из конфигурации ("subject", "total" и т.п.).</summary>
    [MaxLength(64)]
    public string RowKey { get; set; } = "subject";

    public ReportStatus Status { get; set; } = ReportStatus.Draft;

    /// <summary>Версия конфигурации формы, на которой построен снимок.</summary>
    public int FormConfigId { get; set; }
    public ReportFormConfig? FormConfig { get; set; }

    /// <summary>JSON заполненных значений: { "col1": 123.4, "col2": null, ... }.</summary>
    public JsonElement Data { get; set; }

    /// <summary>Результаты проверки правилами валидации на момент сохранения/публикации.</summary>
    public JsonElement? ValidationJson { get; set; }

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedUtc { get; set; } = DateTime.UtcNow;

    [MaxLength(128)]
    public string? UpdatedBy { get; set; }
}
