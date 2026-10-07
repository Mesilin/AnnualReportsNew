using System.ComponentModel.DataAnnotations;

namespace YasenReports.Api.Models;

/// <summary>Субъект РФ (справочник, аналог Catalog.Region основного приложения).</summary>
public class Subject
{
    public Guid Id { get; set; }

    [MaxLength(8)]
    public string Code { get; set; } = "";

    [MaxLength(256)]
    public string Name { get; set; } = "";

    /// <summary>Площадь земель лесного фонда, тыс. га (справочные данные для автозаполнения).</summary>
    public decimal? ForestFundAreaThousandHa { get; set; }

    /// <summary>Порядок сортировки в отчётах.</summary>
    public int? Sorting { get; set; }

    public Guid? FederalDistrictId { get; set; }
    public FederalDistrict? FederalDistrict { get; set; }

    public bool IsActive { get; set; } = true;
}

/// <summary>Федеральный округ (справочник).</summary>
public class FederalDistrict
{
    public Guid Id { get; set; }

    [MaxLength(8)]
    public string Code { get; set; } = "";

    [MaxLength(128)]
    public string Name { get; set; } = "";
}

/// <summary>Авиабаза / авиаотделение региональной базы авиалесоохраны (справочник).</summary>
public class Airbase
{
    public Guid Id { get; set; }

    [MaxLength(16)]
    public string Code { get; set; } = "";

    [MaxLength(256)]
    public string Name { get; set; } = "";

    public Guid? SubjectId { get; set; }
    public Subject? Subject { get; set; }
}

/// <summary>
/// «Замороженные» исходные данные за год: результат выполнения SQL-запросов из конфигурации колонок.
/// Нужен для того, чтобы отчёты за прошлые годы можно было читать без перегенерации
/// (основное приложение может изменить модели и запросы).
/// </summary>
public class SourceDataSnapshot
{
    public int Id { get; set; }

    public int Year { get; set; }

    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }

    /// <summary>Код формы + колонки, к которым относится срез (для справочников — catalog.&lt;name&gt;).</summary>
    [MaxLength(64)]
    public string ColumnKey { get; set; } = "";

    public decimal Value { get; set; }

    public DateTime CapturedUtc { get; set; } = DateTime.UtcNow;
}
