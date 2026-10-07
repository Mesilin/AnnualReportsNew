using System.Text.Json;
using YasenReports.Api.Models;

namespace YasenReports.Api.Data;

/// <summary>
/// Начальные данные: справочники, конфигурации форм (JSON) и правила валидации.
/// Конфигурации соответствуют примерам форм из папки FormExamples:
///  - «Форма 1-авиа» — площадь охраняемых лесов и зоны лесоавиационных работ;
///  - «Форма 2-1-авиа» — дни с полётами, простои, налёт по видам работ.
/// </summary>
public static class SeedData
{
    // Стабильные GUID справочников (для воспроизводимости seed-скриптов миграции).
    public static readonly Guid FdCpo     = Guid.Parse("a0000000-0000-0000-0000-000000000001"); // Центральный
    public static readonly Guid FdSzo     = Guid.Parse("a0000000-0000-0000-0000-000000000002"); // Северо-Западный
    public static readonly Guid FdUfo     = Guid.Parse("a0000000-0000-0000-0000-000000000003"); // Уральский
    public static readonly Guid FdSfo     = Guid.Parse("a0000000-0000-0000-0000-000000000004"); // Сибирский

    public static readonly Guid SubMoscow   = Guid.Parse("b0000000-0000-0000-0000-000000000001");
    public static readonly Guid SubVladimir = Guid.Parse("b0000000-0000-0000-0000-000000000002");
    public static readonly Guid SubKarelia  = Guid.Parse("b0000000-0000-0000-0000-000000000003");
    public static readonly Guid SubKrasnoyarsk = Guid.Parse("b0000000-0000-0000-0000-000000000004");
    public static readonly Guid SubIrkutsk  = Guid.Parse("b0000000-0000-0000-0000-000000000005");

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        WriteIndented = false,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() },
    };

    private static JsonElement ToJson(object o) => JsonDocument.Parse(JsonSerializer.Serialize(o, JsonOpts)).RootElement.Clone();

    public static void Seed(YasenDbContext db)
    {
        if (!db.FederalDistricts.Any())
        {
            db.FederalDistricts.AddRange(
                new FederalDistrict { Id = FdCpo, Code = "ЦФО", Name = "Центральный федеральный округ" },
                new FederalDistrict { Id = FdSzo, Code = "СЗФО", Name = "Северо-Западный федеральный округ" },
                new FederalDistrict { Id = FdUfo, Code = "УФО", Name = "Уральский федеральный округ" },
                new FederalDistrict { Id = FdSfo, Code = "СФО", Name = "Сибирский федеральный округ" });
            db.SaveChanges();
        }

        if (!db.Subjects.Any())
        {
            db.Subjects.AddRange(
                new Subject { Id = SubMoscow, Code = "50", Name = "Московская область", ForestFundAreaThousandHa = 1710.4m, FederalDistrictId = FdCpo },
                new Subject { Id = SubVladimir, Code = "33", Name = "Владимирская область", ForestFundAreaThousandHa = 1961.3m, FederalDistrictId = FdCpo },
                new Subject { Id = SubKarelia, Code = "10", Name = "Республика Карелия", ForestFundAreaThousandHa = 22587.1m, FederalDistrictId = FdSzo },
                new Subject { Id = SubKrasnoyarsk, Code = "24", Name = "Красноярский край", ForestFundAreaThousandHa = 156284.6m, FederalDistrictId = FdSfo },
                new Subject { Id = SubIrkutsk, Code = "38", Name = "Иркутская область", ForestFundAreaThousandHa = 76929.5m, FederalDistrictId = FdSfo });
            db.SaveChanges();
        }

        if (!db.Airbases.Any())
        {
            db.Airbases.AddRange(
                new Airbase { Code = "МРАБ", Name = "Мосская авиабаза (ФГКУ «Мособллес»)", SubjectId = SubMoscow },
                new Airbase { Code = "ВЛАБ", Name = "Владимирская авиабаза", SubjectId = SubVladimir },
                new Airbase { Code = "КАРБ", Name = "Петрозаводская авиабаза", SubjectId = SubKarelia },
                new Airbase { Code = "КРАБ", Name = "Красноярская база авиалесоохраны", SubjectId = SubKrasnoyarsk },
                new Airbase { Code = "ИРБ", Name = "Иркутская база авиалесоохраны", SubjectId = SubIrkutsk });
            db.SaveChanges();
        }

        SeedFormConfigs(db);
        SeedValidationRules(db);
    }

    private static void SeedFormConfigs(YasenDbContext db)
    {
        if (db.FormConfigs.Any()) return;

        var form1 = BuildForm1Avia(2025);
        var form2 = BuildForm21Avia(2025);

        db.FormConfigs.AddRange(
            new ReportFormConfig { Code = "1-avia", Year = 2025, Title = "Форма № 1-авиа. Охрана лесов от пожаров, зоны лесоавиационных работ", Config = ToJson(form1) },
            new ReportFormConfig { Code = "2-1-avia", Year = 2025, Title = "Форма № 2-1-авиа. Дни с полётами, простои, налёт по видам работ", Config = ToJson(form2) });
        db.SaveChanges();
    }

    private static void SeedValidationRules(YasenDbContext db)
    {
        if (db.ValidationRules.Any()) return;

        db.ValidationRules.AddRange(
            // Примеры правил из задания:
            new ValidationRuleConfig
            {
                Year = 2025, FormCode = "1-avia", Kind = ValidationRuleKind.IntraForm,
                Description = "Площадь лесов, фактически охраняемых в ведении субъектов РФ (гр. 8), не должна превышать общую площадь этих лесов (гр. 7)",
                LeftExpression = "col8", RightExpression = "col7",
                Operator = ComparisonOperator.LessOrEqual, IsBlocking = true,
            },
            new ValidationRuleConfig
            {
                Year = 2025, FormCode = "1-avia", Kind = ValidationRuleKind.IntraForm,
                Description = "Сумма площадей зон лесоавиационных работ (гр. 3+4+5) должна быть равна общей площади земель лесного фонда (гр. 1)",
                LeftExpression = "col3+col4+col5", RightExpression = "col1",
                Operator = ComparisonOperator.Equal, Tolerance = 0.5m, IsBlocking = false,
            },
            new ValidationRuleConfig
            {
                Year = 2025, FormCode = "1-avia", Kind = ValidationRuleKind.IntraForm,
                Description = "Общая площадь наземной зоны охраны (гр. 2) не должна превышать общую площадь земель лесного фонда (гр. 1)",
                LeftExpression = "col2", RightExpression = "col1",
                Operator = ComparisonOperator.LessOrEqual, IsBlocking = true,
            },
            // Межформенный контроль: налёт на охрану лесов (2-1-avia.col10) <= общий налёт (1-avia.col12 — всего зона ЛАТР)
            new ValidationRuleConfig
            {
                Year = 2025, FormCode = "2-1-avia", Kind = ValidationRuleKind.InterForm,
                Description = "Налёт по охране лесов от пожаров (2-1-авиа, гр. 10) не должен превышать общий налёт на ЛАТР (1-авиа, гр. 12)",
                LeftExpression = "col10", RightExpression = "1-avia.col12",
                RightFormCode = "1-avia",
                Operator = ComparisonOperator.LessOrEqual, IsBlocking = true,
            },
            new ValidationRuleConfig
            {
                Year = 2025, FormCode = "2-1-avia", Kind = ValidationRuleKind.IntraForm,
                Description = "Сумма дней с полётами по подвидам (гр. 2+3) должна быть равна общему числу дней с полётами (гр. 1)",
                LeftExpression = "col2+col3", RightExpression = "col1",
                Operator = ComparisonOperator.Equal, Tolerance = 0.01m, IsBlocking = false,
            });
        db.SaveChanges();
    }

    // ------------------------------------------------------------------
    // Конфигурация формы 1-авиа (по мотивам «форма 01 авиа 21.mht»)
    // ------------------------------------------------------------------
    private static FormDefinition BuildForm1Avia(int year) => new()
    {
        Code = "1-avia",
        Title = $"Форма № 1-авиа. Основные показатели по авиационной охране лесов за {year} год",
        Periodicity = "Один раз в год",
        UnitNote = "Площади указываются в тыс. га",
        Rows =
        {
            new RowDefinition { Key = "subject", Label = "Субъект Российской Федерации", Kind = RowKindType.Subject, SubjectScoped = true, Catalog = "subject" },
            new RowDefinition { Key = "total", Label = "Всего", Kind = RowKindType.RegionBase, Aggregate = "sum" },
        },
        Columns =
        {
            new ColumnDefinition { Code = "num", Number = "№ п/п", Caption = "Номер строки", Numeric = false, SourceType = ColumnSourceType.Computed, Expression = "$rownum", ReadOnly = true },
            new ColumnDefinition { Code = "name", Number = "Б", Caption = "Наименование авиаотделений, ЛПС", Numeric = false, SourceType = ColumnSourceType.Text, DefaultFromCatalog = "subject.name", ReadOnly = true },
            new ColumnDefinition { Code = "code", Number = "В", Caption = "Код субъекта", Numeric = false, SourceType = ColumnSourceType.Text, DefaultFromCatalog = "subject.code", ReadOnly = true },

            new ColumnDefinition { Code = "col1", Number = "1", Caption = "Общая площадь земель лесного фонда, тыс. га", Unit = "тыс. га",
                SourceType = ColumnSourceType.Manual, DefaultFromCatalog = "subject.forestFundArea", Required = true, Min = 0 },

            new ColumnDefinition { Code = "col2", Number = "2", Caption = "Общая площадь наземной зоны охраны лесов", Unit = "тыс. га",
                SourceType = ColumnSourceType.Manual, Required = true, Min = 0, Group = "Наземная зона" },

            new ColumnDefinition { Code = "col3", Number = "3", Caption = "Зона лесоавиационных работ: авиационное обнаружение и наземное тушение", Unit = "тыс. га",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0, Group = "Зоны ЛАТР" },

            new ColumnDefinition { Code = "col4", Number = "4", Caption = "Зона лесоавиационных работ: авиационное обнаружение и тушение", Unit = "тыс. га",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0, Group = "Зоны ЛАТР" },

            new ColumnDefinition { Code = "col5", Number = "5", Caption = "Зона лесоавиационных работ: исключительное обнаружение космическими средствами", Unit = "тыс. га",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0, Group = "Зоны ЛАТР" },

            new ColumnDefinition { Code = "col6", Number = "6", Caption = "Леса, находящиеся в ведении субъектов РФ — общая площадь", Unit = "тыс. га",
                SourceType = ColumnSourceType.SqlQuery, ReadOnly = true,
                Expression = @"SELECT COALESCE(r.""ForestFondSquare"", 0)::numeric / 1000 FROM ""Catalog"".""Region"" r WHERE r.""RegionId"" = '{subjectId}' AND r.""Year"" = {year}",
                Group = "Леса в ведении субъектов РФ" },

            new ColumnDefinition { Code = "col7", Number = "7", Caption = "Леса, расположенные на землях ООПТ", Unit = "тыс. га",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0 },

            new ColumnDefinition { Code = "col8", Number = "8", Caption = "Леса в ведении субъектов РФ — фактически охранялось", Unit = "тыс. га",
                SourceType = ColumnSourceType.Manual, Required = true, Min = 0, Group = "Леса в ведении субъектов РФ" },

            new ColumnDefinition { Code = "col9", Number = "9", Caption = "Леса на землях обороны и безопасности — фактическая охрана", Unit = "тыс. га",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0 },

            new ColumnDefinition { Code = "col10", Number = "10", Caption = "Леса на землях населённых пунктов — фактическая охрана", Unit = "тыс. га",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0 },

            new ColumnDefinition { Code = "col11", Number = "11", Caption = "Леса на землях иных категорий — фактическая охрана", Unit = "тыс. га",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0 },

            new ColumnDefinition { Code = "col12", Number = "12", Caption = "Всего налёт на лесоавиационных работах", Unit = "часов",
                // Данные о налётах в основном приложении нормализованы (FlightLog -> FireFlight -> FireFlightWork):
                SourceType = ColumnSourceType.SqlQuery, ReadOnly = true,
                Expression = @"SELECT COALESCE(SUM(w.""WorkTime""), 0)::numeric / 60 FROM ""Flight"".""FireFlightWork"" w JOIN ""Flight"".""FireFlight"" ff ON ff.""FireFlightId"" = w.""FireFlightId"" JOIN ""Catalog"".""AirbaseDepartment"" d ON d.""AirbaseDepartmentId"" = ff.""AirbaseDepartmentId"" JOIN ""Catalog"".""Airbase"" a ON a.""AirbaseId"" = d.""AirbaseId"" WHERE a.""RegionId"" = '{subjectId}' AND EXTRACT(YEAR FROM ff.""FlightStartDate"") = {year}",
                Group = "Налёт" },

            new ColumnDefinition { Code = "col13", Number = "13", Caption = "в т. ч. зона контроля", Unit = "часов",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0, Group = "Налёт" },
        },
    };

    // ------------------------------------------------------------------
    // Конфигурация формы 2-1-авиа (по мотивам «форма 02-1 авиа 21.mht»)
    // ------------------------------------------------------------------
    private static FormDefinition BuildForm21Avia(int year) => new()
    {
        Code = "2-1-avia",
        Title = $"Форма № 2-1-авиа. Использование авиации для охраны лесов от пожаров за {year} год",
        Periodicity = "Один раз в год",
        UnitNote = "Налёт — в часах",
        Rows =
        {
            new RowDefinition { Key = "subject", Label = "Субъект Российской Федерации", Kind = RowKindType.Subject, SubjectScoped = true, Catalog = "subject" },
            new RowDefinition { Key = "total", Label = "Всего", Kind = RowKindType.RegionBase, Aggregate = "sum" },
        },
        Columns =
        {
            new ColumnDefinition { Code = "num", Number = "№ п/п", Caption = "Номер строки", Numeric = false, SourceType = ColumnSourceType.Computed, Expression = "$rownum", ReadOnly = true },
            new ColumnDefinition { Code = "name", Number = "Б", Caption = "Наименование авиаотделений (авиационных групп)", Numeric = false, SourceType = ColumnSourceType.Text, DefaultFromCatalog = "subject.name", ReadOnly = true },
            new ColumnDefinition { Code = "code", Number = "В", Caption = "Код субъекта", Numeric = false, SourceType = ColumnSourceType.Text, DefaultFromCatalog = "subject.code", ReadOnly = true },

            new ColumnDefinition { Code = "col1", Number = "1", Caption = "Количество дней с полётами, всего", Unit = "дней",
                // Автоподбор из нормализованных данных основного приложения (полёты по пожарам):
                SourceType = ColumnSourceType.SqlQuery, ReadOnly = true, Min = 0,
                Expression = @"SELECT COUNT(DISTINCT DATE(fl.""DateTime"")) FROM ""Flight"".""FlightLog"" fl JOIN ""Flight"".""FireFlight"" ff ON ff.""FireFlightId"" = fl.""FireFlightId"" JOIN ""Catalog"".""AirbaseDepartment"" d ON d.""AirbaseDepartmentId"" = ff.""AirbaseDepartmentId"" JOIN ""Catalog"".""Airbase"" a ON a.""AirbaseId"" = d.""AirbaseId"" WHERE a.""RegionId"" = '{subjectId}' AND fl.""IsDeleted"" = false AND EXTRACT(YEAR FROM fl.""DateTime"") = {year}",
                Group = "Дни с полётами" },

            new ColumnDefinition { Code = "col2", Number = "2", Caption = "в т. ч. с обнаружением пожаров", Unit = "дней",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0, Group = "Дни с полётами" },

            new ColumnDefinition { Code = "col3", Number = "3", Caption = "для осмотра действующих пожаров", Unit = "дней",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0, Group = "Дни с полётами" },

            new ColumnDefinition { Code = "col4", Number = "4", Caption = "Простои по вине авиапредприятий", Unit = "дней",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0, Group = "Дни без полётов" },

            new ColumnDefinition { Code = "col5", Number = "5", Caption = "По погодным условиям", Unit = "дней",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0, Group = "Дни без полётов" },

            new ColumnDefinition { Code = "col6", Number = "6", Caption = "По прочим причинам", Unit = "дней",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0, Group = "Дни без полётов" },

            new ColumnDefinition { Code = "col7", Number = "7", Caption = "Отсутствие необходимости", Unit = "дней",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0, Group = "Дни без полётов" },

            new ColumnDefinition { Code = "col8", Number = "8", Caption = "Налёт всего", Unit = "часов",
                SourceType = ColumnSourceType.Computed, Expression = "col9+col11", ReadOnly = true, Group = "Налёт ВС" },

            new ColumnDefinition { Code = "col9", Number = "9", Caption = "Охрана лесов от пожаров — всего", Unit = "часов",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0, Group = "Налёт ВС" },

            new ColumnDefinition { Code = "col10", Number = "10", Caption = "в т. ч. борьба с пожарами", Unit = "часов",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0, Group = "Налёт ВС" },

            new ColumnDefinition { Code = "col11", Number = "11", Caption = "Прочие производственные полёты (в т. ч. тренировки)", Unit = "часов",
                SourceType = ColumnSourceType.Manual, Min = 0, DefaultValue = 0, Group = "Налёт ВС" },
        },
    };
}
