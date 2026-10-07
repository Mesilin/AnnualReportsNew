using System.ComponentModel.DataAnnotations;

namespace YasenReportsDemo.Models;

// ===================== СПРАВОЧНИКИ (аналог справочников основного приложения) =====================

/// <summary>Субъект РФ</summary>
public class Region
{
    public int Id { get; set; }
    [MaxLength(200)] public string Name { get; set; } = "";
    /// <summary>OKTMO код (пример данных из справочника)</summary>
    [MaxLength(11)] public string? Oktmo { get; set; }
}

/// <summary>Авиаотделение / ЛПС базы авиационной охраны лесов</summary>
public class AirDivision
{
    public int Id { get; set; }
    [MaxLength(200)] public string Name { get; set; } = "";
    public int RegionId { get; set; }
    public Region? Region { get; set; }
    /// <summary>Код авиаотделения (гр. «Коды» в формах)</summary>
    [MaxLength(10)] public string Code { get; set; } = "";
}

/// <summary>Лесничество</summary>
public class Forestry
{
    public int Id { get; set; }
    [MaxLength(200)] public string Name { get; set; } = "";
    public int RegionId { get; set; }
    public Region? Region { get; set; }
    /// <summary>Площадь земель лесного фонда, тыс. га (из справочника зонирования)</summary>
    public double AreaThsHa { get; set; }
}

/// <summary>Пожар (нормализованные данные динамики из основного приложения)</summary>
public class Fire
{
    public int Id { get; set; }
    public int ForestryId { get; set; }
    public Forestry? Forestry { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    /// <summary>Площадь, га</summary>
    public double AreaHa { get; set; }
    /// <summary>Найдено: "авиа" | "назем"</summary>
    [MaxLength(20)] public string DetectedBy { get; set; } = "авиа";
    /// <summary>Ликвидировано: "авиа" | "назем"</summary>
    [MaxLength(20)] public string ExtinguishedBy { get; set; } = "авиа";
}

/// <summary>Налёт часов по видам работ (нормализованные данные из основного приложения)</summary>
public class FlightHour
{
    public int Id { get; set; }
    public int AirDivisionId { get; set; }
    public AirDivision? AirDivision { get; set; }
    public int Year { get; set; }
    /// <summary>Вид работы: "firePatrol" (охрана лесов от пожаров), "other" (прочие производственные полёты)</summary>
    [MaxLength(50)] public string WorkType { get; set; } = "firePatrol";
    /// <summary>Подтип: "total", "training", "patrolDetected", "inspection"... — зависит от задачи вычисляемого поля</summary>
    [MaxLength(50)] public string SubType { get; set; } = "total";
    public double Hours { get; set; }
    public int DaysWithFlights { get; set; }
}

// ===================== КОНФИГУРАЦИЯ ФОРМ И ПРАВИЛ =====================

/// <summary>Версия конфигурации формы (JSON). Форма за год может меняться — добавляем новую версию.</summary>
public class FormConfigVersion
{
    public int Id { get; set; }
    /// <summary>Ключ формы, напр. "form-1-avia"</summary>
    [MaxLength(100)] public string FormKey { get; set; } = "";
    /// <summary>Номер версии конфигурации</summary>
    public int Version { get; set; }
    /// <summary>Год, к которому применена эта версия (форма действует с этого года)</summary>
    public int EffectiveFromYear { get; set; }
    /// <summary>Полный JSON конфигурации формы (строки, колонки, источники данных)</summary>
    public string ConfigJson { get; set; } = "{}";
    public bool IsActive { get; set; } = true;
    /// <summary>Признак архива: старые версии не удаляем — по ним читаются закрытые отчёты</summary>
    public bool IsArchived { get; set; }
}

/// <summary>Правило валидации (внутриформенное или межформенное). Хранится выражение NCalc + человекочитаемое описание.</summary>
public class ValidationRule
{
    public int Id { get; set; }
    /// <summary>FormKey формы-инициатора правила ("*" для глобальных)</summary>
    [MaxLength(100)] public string FormKey { get; set; } = "";
    [MaxLength(300)] public string Description { get; set; } = "";
    /// <summary>intra (внутри строки формы) | cross (межформенное)</summary>
    [MaxLength(20)] public string Scope { get; set; } = "intra";
    /// <summary>expression | sumEq | le | ge ... тип проверки; expression — вычисляется Expression</summary>
    [MaxLength(30)] public string Kind { get; set; } = "expression";
    /// <summary>Выражение NCalc. Переменные — ключи колонок текущей строки ("c1","c2"), либо ключи других форм с префиксом formKey ("form-2-avia.c1").</summary>
    public string? Expression { get; set; }
    /// <summary>Сообщение об ошибке</summary>
    [MaxLength(500)] public string ErrorMessage { get; set; } = "";
    /// <summary>error | warning</summary>
    [MaxLength(10)] public string Severity { get; set; } = "error";
    public bool Enabled { get; set; } = true;
    public int EffectiveFromYear { get; set; }
    public int SortOrder { get; set; }
}

// ===================== ДАННЫЕ ОТЧЁТОВ =====================

/// <summary>Заголовок отчёта (экземпляр формы) за конкретный год.</summary>
public class ReportInstance
{
    public int Id { get; set; }
    [MaxLength(100)] public string FormKey { get; set; } = "";
    public int Year { get; set; }
    public int RegionId { get; set; }
    public Region? Region { get; set; }
    /// <summary>draft | submitted | approved (finalize — «заморозка»: данные больше не пересчитываются)</summary>
    [MaxLength(20)] public string Status { get; set; } = "draft";
    /// <summary>Версия конфигурации формы, на которой создан отчёт (для воспроизведения внешнего вида)</summary>
    public int ConfigVersionId { get; set; }
    public FormConfigVersion? ConfigVersion { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? FinalizedAt { get; set; }
    public List<ReportRow> Rows { get; set; } = new();
}

/// <summary>Строка таблицы отчёта (например, строка авиаотделения).</summary>
public class ReportRow
{
    public int Id { get; set; }
    public int ReportInstanceId { get; set; }
    public ReportInstance? ReportInstance { get; set; }
    /// <summary>Ключ строки из конфига (id авиаотделения и т.п.)</summary>
    [MaxLength(100)] public string RowKey { get; set; } = "";
    /// <summary>Значения ячеек ручного ввода/справочников: {"c1": 123.4, "c2": null}</summary>
    public string ValuesJson { get; set; } = "{}";
    /// <summary>Отпечаток вычисляемых значений на момент сохранения/финализации (чтобы при падении SQL-запроса читать сохранённое)</summary>
    public string ComputedSnapshotJson { get; set; } = "{}";
    public int SortOrder { get; set; }
}
