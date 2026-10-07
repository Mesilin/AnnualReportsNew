using System.Text.Json.Serialization;

namespace YasenReports.Api.Models;

/// <summary>
/// C#-модель JSON-конфигурации формы (сериализуется в ReportFormConfig.Config).
/// Пример см. в Data/SeedData.cs.
/// </summary>
public class FormDefinition
{
    [JsonPropertyName("code")] public string Code { get; set; } = "";
    [JsonPropertyName("title")] public string Title { get; set; } = "";
    [JsonPropertyName("periodicity")] public string Periodicity { get; set; } = "Один раз в год";
    [JsonPropertyName("unitNote")] public string? UnitNote { get; set; }

    /// <summary>Строки формы. Для «субъектной» формы обычно одна строка с subjectScoped=true.</summary>
    [JsonPropertyName("rows")] public List<RowDefinition> Rows { get; set; } = new();

    /// <summary>Колонки формы (порядок = порядок вывода).</summary>
    [JsonPropertyName("columns")] public List<ColumnDefinition> Columns { get; set; } = new();
}

public class RowDefinition
{
    /// <summary>Машинный ключ строки: "subject", "total".</summary>
    [JsonPropertyName("key")] public string Key { get; set; } = "";

    [JsonPropertyName("label")] public string Label { get; set; } = "";

    /// <summary>Тип строки: субъект / ФО / база.</summary>
    [JsonPropertyName("kind")] public RowKindType Kind { get; set; } = RowKindType.Subject;

    /// <summary>Если true — строка создаётся отдельно по каждому активному субъекту.</summary>
    [JsonPropertyName("subjectScoped")] public bool SubjectScoped { get; set; }

    /// <summary>Для сводных строк: как агрегировать значения колонок ("sum" | null).</summary>
    [JsonPropertyName("aggregate")] public string? Aggregate { get; set; }

    /// <summary>Справочник, из которого берётся подпись строки (subject|airbase).</summary>
    [JsonPropertyName("catalog")] public string? Catalog { get; set; }
}

public class ColumnDefinition
{
    /// <summary>Машинный код колонки, используется в правилах валидации: col1..colN, name и т.п.</summary>
    [JsonPropertyName("code")] public string Code { get; set; } = "";

    /// <summary>Номер графы по форме отчёта (для отображения), либо null для служебных колонок.</summary>
    [JsonPropertyName("number")] public string? Number { get; set; }

    [JsonPropertyName("caption")] public string Caption { get; set; } = "";

    /// <summary>Единица измерения, напр. "тыс. га", "часов", "дней".</summary>
    [JsonPropertyName("unit")] public string? Unit { get; set; }

    [JsonPropertyName("sourceType")] public ColumnSourceType SourceType { get; set; } = ColumnSourceType.Manual;

    /// <summary>
    /// Для SqlQuery — SQL к запросу к БД основного приложения (нормализованные данные пожаров/налётов).
    /// Плейсхолдеры: {year}, {subjectId}. Должен возвращать скаляр.
    /// Для Computed — формула над кодами колонок этой же строки, напр. "col3+col4+col5".
    /// </summary>
    [JsonPropertyName("expression")] public string? Expression { get; set; }

    /// <summary>Для Manual — брать ли начальное значение из справочника локальной БД (subject.forestFundArea).</summary>
    [JsonPropertyName("defaultFromCatalog")] public string? DefaultFromCatalog { get; set; }

    [JsonPropertyName("required")] public bool Required { get; set; }

    [JsonPropertyName("readOnly")] public bool ReadOnly { get; set; }

    /// <summary>Числовая колонка или текстовая.</summary>
    [JsonPropertyName("numeric")] public bool Numeric { get; set; } = true;

    /// <summary>Минимум/максимум для ручной проверки ввода.</summary>
    [JsonPropertyName("min")] public decimal? Min { get; set; }
    [JsonPropertyName("max")] public decimal? Max { get; set; }

    /// <summary>Значение по умолчанию (например 0 для незаполненных граф).</summary>
    [JsonPropertyName("defaultValue")] public decimal? DefaultValue { get; set; }

    /// <summary>Группа колонок для визуальной группировки заголовков (необязательно).</summary>
    [JsonPropertyName("group")] public string? Group { get; set; }
}
