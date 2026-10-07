namespace YasenReportsDemo.Models;

// ===== DTO структуры JSON-конфигурации формы (десериализуется из FormConfigVersion.ConfigJson) =====

/// <summary>Верхний уровень конфигурации формы</summary>
public class FormConfig
{
    public string FormKey { get; set; } = "";
    public int Version { get; set; }
    /// <summary>Отображаемый номер формы, напр. "1-авиа"</summary>
    public string FormNumber { get; set; } = "";
    public string Title { get; set; } = "";
    /// <summary>Группировка строк: "airDivision" | "forestry" | "region"</summary>
    public string RowSource { get; set; } = "airDivision";
    /// <summary>Параметры строки (напр. @year, @regionId) доступны внутри SqlSource/DictSource</summary>
    public List<ColumnConfig> Columns { get; set; } = new();
}

public enum ColumnKind { Manual, Dict, Computed, Formula }

public class ColumnConfig
{
    /// <summary>Машинный ключ колонки: "c1", "c2"... Используется в правилах валидации и межформенном контроле.</summary>
    public string Key { get; set; } = "";
    /// <summary>Заголовок (гр.) как в бумажной форме</summary>
    public string Header { get; set; } = "";
    /// <summary>Номер графы ("1","2","3") для шапки таблицы</summary>
    public string? Grafa { get; set; }
    public ColumnKind Kind { get; set; } = ColumnKind.Manual;
    public string DataType { get; set; } = "decimal"; // decimal | int | text
    public bool Required { get; set; }
    public double? MinValue { get; set; }
    public double? MaxValue { get; set; }
    public int Decimals { get; set; } = 1;

    /// <summary>Для Kind=Dict: SQL-запрос со плейсхолдерами @regionId/@year, возвращающий (key, value)</summary>
    public string? DictSql { get; set; }

    /// <summary>Для Kind=Computed: SQL-запрос со плейсхолдерами @regionId,@year,@rowKey, возвращающее одно скалярное значение</summary>
    public string? SqlSource { get; set; }

    /// <summary>Для Kind=Formula: выражение NCalc по другим колонкам строки, напр. "([c3] + [c4] + [c5]) == [c1]" — только для display-проверок на клиенте не используем, это про вычисляемые суммы ячеек: напр. "[c1] - [c2]".</summary>
    public string? Expression { get; set; }

    /// <summary>Итоговая строка: sum (сумма по колонке), none</summary>
    public string Total { get; set; } = "none";

    /// <summary>Подсказка пользователю</summary>
    public string? Hint { get; set; }
}
