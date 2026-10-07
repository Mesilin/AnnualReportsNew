namespace YasenReports.Api.Models;

/// <summary>Способ получения значения ячейки формы.</summary>
public enum ColumnSourceType
{
    /// <summary>Значение вводит пользователь вручную.</summary>
    Manual = 0,

    /// <summary>Значение берётся из SQL-запроса, указанного в конфигурации колонки ({0} — год, {1} — id субъекта).</summary>
    SqlQuery = 1,

    /// <summary>Вычисляемая колонка: сумма других колонок этой же строки (Expression = "col3+col4+col5").</summary>
    Computed = 2,

    /// <summary>Только текстовое значение (наименование и т.п.).</summary>
    Text = 3,
}

/// <summary>Тип строки формы: субъект РФ или сводная по федеральному округу/региону.</summary>
public enum RowKindType
{
    Subject = 0,
    FederalDistrict = 1,
    RegionBase = 2,
}

/// <summary>Вид правила валидации.</summary>
public enum ValidationRuleKind
{
    /// <summary>Внутриформенное: LeftExpr <op> RightExpr, обе стороны вычисляются на одной строке одной формы.</summary>
    IntraForm = 0,

    /// <summary>Межформенное: LeftExpr (форма Left) <op> RightExpr (форма Right), сравнение по одному субъекту/строке.</summary>
    InterForm = 1,
}

/// <summary>Операция сравнения.</summary>
public enum ComparisonOperator
{
    LessOrEqual = 0, // <=
    GreaterOrEqual = 1, // >=
    Equal = 2, // =
    Less = 3, // <
    Greater = 4, // >
}

/// <summary>Статус сохранённого отчёта.</summary>
public enum ReportStatus
{
    Draft = 0,
    Published = 1,
}
