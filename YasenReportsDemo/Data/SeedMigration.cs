using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using YasenReportsDemo.Models;

namespace YasenReportsDemo.Data;

/// <summary>
/// Seed-миграция: создаёт тестовые справочники, данные о пожарах/налётах,
/// конфигурации форм (JSON) и правила валидации. Всё — через БД-миграции, как просил пользователь.
/// </summary>
public partial class SeedInitialData : Migration
{
    // ================= JSON-КОНФИГУРАЦИИ ФОРМ =================

    const string Form1V2024 = """
{
  "formKey": "form-1-avia",
  "version": 1,
  "formNumber": "1-авиа",
  "title": "Распределение площадей земель лесного фонда по зонам лесоавиационных работ",
  "rowSource": "airDivision",
  "columns": [
    { "key": "c1", "header": "Общая площадь земель лесного фонда, тыс. га (по приказу о лесопожарном зонировании)", "grafa": "1",
      "kind": "Computed", "dataType": "decimal", "total": "sum",
      "sqlSource": "SELECT COALESCE(SUM(f.\"AreaThsHa\"), 0) FROM \"Forestries\" f WHERE f.\"RegionId\" = @regionId" },

    { "key": "c2", "header": "Обслуживаемая по госзаданиям, договорам площадь лесов, тыс. га", "grafa": "2",
      "kind": "Manual", "dataType": "decimal", "required": true, "minValue": 0, "maxValue": 999999, "decimals": 1, "total": "sum" },

    { "key": "c3", "header": "Леса в ведении субъектов РФ: фактически охранялось, тыс. га", "grafa": "3",
      "kind": "Manual", "dataType": "decimal", "required": true, "minValue": 0, "decimals": 1, "total": "sum" },

    { "key": "c4", "header": "Зона авиационного обнаружения и наземного тушения, тыс. га", "grafa": "4",
      "kind": "Manual", "dataType": "decimal", "required": true, "minValue": 0, "decimals": 1, "total": "sum" },

    { "key": "c5", "header": "Зона авиационного обнаружения и тушения, тыс. га", "grafa": "5",
      "kind": "Manual", "dataType": "decimal", "required": true, "minValue": 0, "decimals": 1, "total": "sum" },

    { "key": "c6", "header": "Зона исключительного обнаружения с помощью космических средств, тыс. га", "grafa": "6",
      "kind": "Manual", "dataType": "decimal", "required": true, "minValue": 0, "decimals": 1, "total": "sum" },

    { "key": "c7", "header": "Зона контроля, тыс. га", "grafa": "7",
      "kind": "Manual", "dataType": "decimal", "required": true, "minValue": 0, "decimals": 1, "total": "sum" },

    { "key": "c8", "header": "Площадь лесов ООПТ (заповедники, нацпарки, заказники), тыс. га", "grafa": "8",
      "kind": "Dict", "dataType": "decimal", "decimals": 1, "total": "sum",
      "hint": "Справочник ООПТ (в демо — подстрока SQL к справочнику лесничеств)",
      "dictSql": "SELECT 'value' AS key, COALESCE(SUM(f.\"AreaThsHa\" * 0.05), 0) AS value FROM \"Forestries\" f WHERE f.\"RegionId\" = @regionId" }
  ]
}
""";

    /// <summary>Версия формы 1-авиа для 2026 года: косметика заголовков + новая колонка c9, удалена неактуальная подсказка.</summary>
    const string Form1V2026 = """
{
  "formKey": "form-1-avia",
  "version": 2,
  "formNumber": "1-авиа",
  "title": "Распределение площадей земель лесного фонда по зонам лесоавиационных работ (ред. 2026)",
  "rowSource": "airDivision",
  "columns": [
    { "key": "c1", "header": "Общая площадь лесного фонда, тыс. га", "grafa": "1",
      "kind": "Computed", "dataType": "decimal", "total": "sum",
      "sqlSource": "SELECT COALESCE(SUM(f.\"AreaThsHa\"), 0) FROM \"Forestries\" f WHERE f.\"RegionId\" = @regionId" },

    { "key": "c2", "header": "Площадь лесов по госзаданию, тыс. га", "grafa": "2",
      "kind": "Manual", "dataType": "decimal", "required": true, "minValue": 0, "maxValue": 999999, "decimals": 1, "total": "sum" },

    { "key": "c3", "header": "Фактически охраняемая площадь в ведении субъектов РФ, тыс. га", "grafa": "3",
      "kind": "Manual", "dataType": "decimal", "required": true, "minValue": 0, "decimals": 1, "total": "sum" },

    { "key": "c4", "header": "Зона АБ и наземного тушения, тыс. га", "grafa": "4",
      "kind": "Manual", "dataType": "decimal", "required": true, "minValue": 0, "decimals": 1, "total": "sum" },

    { "key": "c5", "header": "Зона АБ и тушения, тыс. га", "grafa": "5",
      "kind": "Manual", "dataType": "decimal", "required": true, "minValue": 0, "decimals": 1, "total": "sum" },

    { "key": "c6", "header": "Зона космического обнаружения, тыс. га", "grafa": "6",
      "kind": "Manual", "dataType": "decimal", "required": true, "minValue": 0, "decimals": 1, "total": "sum" },

    { "key": "c7", "header": "Зона контроля, тыс. га", "grafa": "7",
      "kind": "Manual", "dataType": "decimal", "required": true, "minValue": 0, "decimals": 1, "total": "sum" },

    { "key": "c9", "header": "Площадь лесов на землях обороны и безопасности, тыс. га", "grafa": "8",
      "kind": "Manual", "dataType": "decimal", "minValue": 0, "decimals": 1, "total": "sum",
      "hint": "НОВЫЙ столбец редакции 2026 г." },

    { "key": "c8", "header": "Площадь лесов ООПТ, тыс. га", "grafa": "9",
      "kind": "Dict", "dataType": "decimal", "decimals": 1, "total": "sum",
      "dictSql": "SELECT 'value' AS key, COALESCE(SUM(f.\"AreaThsHa\" * 0.05), 0) AS value FROM \"Forestries\" f WHERE f.\"RegionId\" = @regionId" }
  ]
}
""";

    const string Form2V2024 = """
{
  "formKey": "form-2-avia",
  "version": 1,
  "formNumber": "2-1 авиация",
  "title": "Налёт часов воздушных судов по видам работ",
  "rowSource": "airDivision",
  "columns": [
    { "key": "h1", "header": "Кол-во дней с полётами, всего", "grafa": "1",
      "kind": "Computed", "dataType": "int", "total": "sum",
      "sqlSource": "SELECT COALESCE(SUM(fh.\"DaysWithFlights\"), 0) FROM \"FlightHours\" fh WHERE fh.\"AirDivisionId\" = @rowKey AND fh.\"Year\" = @year" },

    { "key": "h2", "header": "Налёт по охране лесов от пожаров, всего, часов", "grafa": "2",
      "kind": "Computed", "dataType": "decimal", "total": "sum",
      "sqlSource": "SELECT COALESCE(SUM(fh.\"Hours\"), 0) FROM \"FlightHours\" fh WHERE fh.\"AirDivisionId\" = @rowKey AND fh.\"Year\" = @year AND fh.\"WorkType\" = 'firePatrol' AND fh.\"SubType\" = 'total'" },

    { "key": "h3", "header": "в т.ч. патрулирование с обнаружением пожаров, часов", "grafa": "3",
      "kind": "Computed", "dataType": "decimal", "total": "sum",
      "sqlSource": "SELECT COALESCE(SUM(fh.\"Hours\"), 0) FROM \"FlightHours\" fh WHERE fh.\"AirDivisionId\" = @rowKey AND fh.\"Year\" = @year AND fh.\"SubType\" = 'patrolDetected'" },

    { "key": "h4", "header": "в т.ч. осмотр действующих пожаров, часов", "grafa": "4",
      "kind": "Computed", "dataType": "decimal", "total": "sum",
      "sqlSource": "SELECT COALESCE(SUM(fh.\"Hours\"), 0) FROM \"FlightHours\" fh WHERE fh.\"AirDivisionId\" = @rowKey AND fh.\"Year\" = @year AND fh.\"SubType\" = 'inspection'" },

    { "key": "h5", "header": "Прочие производственные полёты, часов", "grafa": "5",
      "kind": "Computed", "dataType": "decimal", "total": "sum",
      "sqlSource": "SELECT COALESCE(SUM(fh.\"Hours\"), 0) FROM \"FlightHours\" fh WHERE fh.\"AirDivisionId\" = @rowKey AND fh.\"Year\" = @year AND fh.\"WorkType\" = 'other'" },

    { "key": "h6", "header": "Тренировочные полёты, часов", "grafa": "6",
      "kind": "Manual", "dataType": "decimal", "required": true, "minValue": 0, "decimals": 1, "total": "sum" },

    { "key": "h7", "header": "Простой по вине авиапредприятия, дней", "grafa": "7",
      "kind": "Manual", "dataType": "int", "required": false, "minValue": 0, "decimals": 0, "total": "sum" }
  ]
}
""";

    const string Form3V2024 = """
{
  "formKey": "form-3-fires",
  "version": 1,
  "formNumber": "3-пожары",
  "title": "Количество и площадь пожаров по лесничествам",
  "rowSource": "forestry",
  "columns": [
    { "key": "f1", "header": "Количество пожаров, шт.", "grafa": "1",
      "kind": "Computed", "dataType": "int", "total": "sum",
      "sqlSource": "SELECT COUNT(*) FROM \"Fires\" fr WHERE fr.\"ForestryId\" = @rowKey AND EXTRACT(YEAR FROM fr.\"StartDate\") = @year" },

    { "key": "f2", "header": "Площадь, покрытая огнём, га", "grafa": "2",
      "kind": "Computed", "dataType": "decimal", "total": "sum",
      "sqlSource": "SELECT COALESCE(SUM(fr.\"AreaHa\"), 0) FROM \"Fires\" fr WHERE fr.\"ForestryId\" = @rowKey AND EXTRACT(YEAR FROM fr.\"StartDate\") = @year" },

    { "key": "f3", "header": "Площадь погибшего леса, га", "grafa": "3",
      "kind": "Manual", "dataType": "decimal", "required": true, "minValue": 0, "decimals": 1, "total": "sum" },

    { "key": "f4", "header": "Количество пожаров, обнаруженных авиацией, шт.", "grafa": "4",
      "kind": "Computed", "dataType": "int", "total": "sum",
      "sqlSource": "SELECT COUNT(*) FROM \"Fires\" fr WHERE fr.\"ForestryId\" = @rowKey AND fr.\"DetectedBy\" = 'авиа' AND EXTRACT(YEAR FROM fr.\"StartDate\") = @year" },

    { "key": "f5", "header": "В том числе ликвидировано силами авиаподразделений, шт.", "grafa": "5",
      "kind": "Computed", "dataType": "int", "total": "sum",
      "sqlSource": "SELECT COUNT(*) FROM \"Fires\" fr WHERE fr.\"ForestryId\" = @rowKey AND fr.\"ExtinguishedBy\" = 'авиа' AND EXTRACT(YEAR FROM fr.\"StartDate\") = @year" }
  ]
}
""";

    // ================= ПРАВИЛА ВАЛИДАЦИИ =================

    static IEnumerable<ValidationRule> Rules() => new[]
    {
        new ValidationRule
        {
            FormKey = "form-1-avia", Scope = "intra", Kind = "expression", Enabled = true, EffectiveFromYear = 2024, SortOrder = 1,
            Description = "Площадь лесов, фактически охраняемых в ведении субъектов РФ (гр.3), не должна превышать общую площадь этих лесов (гр.2)",
            Expression = "[c3] <= [c2]",
            ErrorMessage = "Гр.3 (фактически охраняемое в ведении субъектов) не может превышать гр.2 (обслуживаемая площадь)"
        },
        new ValidationRule
        {
            FormKey = "form-1-avia", Scope = "intra", Kind = "expression", Enabled = true, EffectiveFromYear = 2024, SortOrder = 2,
            Description = "Сумма площадей зон лесоавиационных работ (гр. 4+5+6) должна быть равна общей площади земель лесного фонда (гр. 1)",
            Expression = "Abs(([c4] + [c5] + [c6]) - [c1]) < 0.01",
            ErrorMessage = "Сумма граф 4+5+6 должна равняться графе 1 (общая площадь лесного фонда)"
        },
        new ValidationRule
        {
            FormKey = "form-1-avia", Scope = "intra", Kind = "expression", Enabled = true, EffectiveFromYear = 2024, SortOrder = 3,
            Description = "Зона контроля (гр.7) входит в состав зоны обнаружения и наземного тушения (гр.4): гр.7 <= гр.4",
            Expression = "[c7] <= [c4]",
            ErrorMessage = "Графа 7 (зона контроля) не должна превышать графу 4"
        },
        new ValidationRule
        {
            FormKey = "form-2-avia", Scope = "intra", Kind = "expression", Enabled = true, EffectiveFromYear = 2024, SortOrder = 10,
            Description = "Часы на борьбу с пожарами (гр.3+гр.4) не должны превышать общий налёт по охране лесов (гр.2)",
            Expression = "([h3] + [h4]) <= [h2]",
            ErrorMessage = "Сумма граф 3+4 не должна превышать графу 2 (налёт по охране лесов)"
        },
        new ValidationRule
        {
            // Межформенный контроль: сумма h2 по форме 2-авиа >= суммы f4 (кол-во обнаруженных авиацией пожаров) формы 3 по региону
            FormKey = "form-3-fires", Scope = "cross", Kind = "expression", Enabled = true, EffectiveFromYear = 2024, SortOrder = 20,
            Description = "Межформенный контроль: количество пожаров, обнаруженных авиацией (форма 3, гр.4), не должно превышать число вылетов на обнаружение (форма 2-1 авиация, гр.3)",
            Expression = "[SUM_f4] <= [form-2-avia.SUM_h3]",
            ErrorMessage = "Обнаружено авиацией больше пожаров, чем было патрулирований с обнаружением в форме 2-1"
        },
        new ValidationRule
        {
            FormKey = "form-3-fires", Scope = "intra", Kind = "expression", Enabled = true, EffectiveFromYear = 2024, SortOrder = 21,
            Description = "Ликвидировано авиаподразделениями (гр.5) не больше, чем обнаружено авиацией (гр.4)",
            Expression = "[f5] <= [f4]",
            ErrorMessage = "Графа 5 (ликвидировано авиацией) не должна превышать графу 4 (обнаружено авиацией)"
        },
        new ValidationRule
        {
            FormKey = "form-3-fires", Scope = "intra", Kind = "expression", Enabled = true, EffectiveFromYear = 2024, SortOrder = 22,
            Description = "Площадь погибшего леса (гр.3) не должна превышать площадь, покрытую огнём (гр.2)",
            Expression = "[f3] <= [f2]",
            ErrorMessage = "Площадь погибшего леса не может превышать пройденную огнём площадь"
        }
    };

    // ================= ДАННЫЕ =================

    static IEnumerable<Region> RegionsSeed() => new[]
    {
        new Region { Id = 1, Name = "Красноярский край", Oktmo = "04000000" },
        new Region { Id = 2, Name = "Иркутская область", Oktmo = "25000000" },
        new Region { Id = 3, Name = "Якутия (Республика Саха)", Oktmo = "98000000" }
    };

    static IEnumerable<AirDivision> DivisionsSeed() => new[]
    {
        new AirDivision { Id = 1, Name = "Красноярское авиаотделение №1", RegionId = 1, Code = "2401" },
        new AirDivision { Id = 2, Name = "Красноярское авиаотделение №2 (Енисейск)", RegionId = 1, Code = "2402" },
        new AirDivision { Id = 3, Name = "Иркутское авиаотделение №1", RegionId = 2, Code = "3801" },
        new AirDivision { Id = 4, Name = "Якутское авиаотделение №1", RegionId = 3, Code = "1401" }
    };

    static IEnumerable<Forestry> ForestriesSeed() => new[]
    {
        new Forestry { Id = 1, Name = "Красноярское лесничество", RegionId = 1, AreaThsHa = 1200.5 },
        new Forestry { Id = 2, Name = "Емельяновское лесничество", RegionId = 1, AreaThsHa = 850.3 },
        new Forestry { Id = 3, Name = "Иркутское лесничество", RegionId = 2, AreaThsHa = 980.0 },
        new Forestry { Id = 4, Name = "Якутское лесничество", RegionId = 3, AreaThsHa = 2500.7 }
    };

    static IEnumerable<Fire> FiresSeed() => new[]
    {
        new Fire { Id = 1, ForestryId = 1, StartDate = new DateTime(2024, 5, 12), EndDate = new DateTime(2024, 5, 18), AreaHa = 150.0, DetectedBy = "авиа", ExtinguishedBy = "авиа" },
        new Fire { Id = 2, ForestryId = 1, StartDate = new DateTime(2024, 6, 3), EndDate = new DateTime(2024, 6, 10), AreaHa = 320.5, DetectedBy = "авиа", ExtinguishedBy = "назем" },
        new Fire { Id = 3, ForestryId = 2, StartDate = new DateTime(2024, 7, 1), EndDate = new DateTime(2024, 7, 5), AreaHa = 80.0, DetectedBy = "назем", ExtinguishedBy = "назем" },
        new Fire { Id = 4, ForestryId = 3, StartDate = new DateTime(2024, 5, 20), EndDate = new DateTime(2024, 5, 25), AreaHa = 210.0, DetectedBy = "авиа", ExtinguishedBy = "авиа" },
        new Fire { Id = 5, ForestryId = 4, StartDate = new DateTime(2024, 6, 15), EndDate = new DateTime(2024, 6, 30), AreaHa = 1200.0, DetectedBy = "авиа", ExtinguishedBy = "авиа" },
        // 2025 год — для проверки «генерируем текущий, читаем прошлые»
        new Fire { Id = 6, ForestryId = 1, StartDate = new DateTime(2025, 5, 8), EndDate = new DateTime(2025, 5, 12), AreaHa = 95.0, DetectedBy = "авиа", ExtinguishedBy = "авиа" },
        new Fire { Id = 7, ForestryId = 3, StartDate = new DateTime(2025, 6, 2), EndDate = null, AreaHa = 410.0, DetectedBy = "авиа", ExtinguishedBy = "авиа" }
    };

    static IEnumerable<FlightHour> FlightHoursSeed() => new[]
    {
        new FlightHour { Id = 1, AirDivisionId = 1, Year = 2024, WorkType = "firePatrol", SubType = "total", Hours = 420.5, DaysWithFlights = 62 },
        new FlightHour { Id = 2, AirDivisionId = 1, Year = 2024, WorkType = "firePatrol", SubType = "patrolDetected", Hours = 260.0, DaysWithFlights = 0 },
        new FlightHour { Id = 3, AirDivisionId = 1, Year = 2024, WorkType = "firePatrol", SubType = "inspection", Hours = 85.5, DaysWithFlights = 0 },
        new FlightHour { Id = 4, AirDivisionId = 1, Year = 2024, WorkType = "other", SubType = "total", Hours = 50.0, DaysWithFlights = 0 },
        new FlightHour { Id = 5, AirDivisionId = 2, Year = 2024, WorkType = "firePatrol", SubType = "total", Hours = 310.0, DaysWithFlights = 45 },
        new FlightHour { Id = 6, AirDivisionId = 2, Year = 2024, WorkType = "firePatrol", SubType = "patrolDetected", Hours = 190.0, DaysWithFlights = 0 },
        new FlightHour { Id = 7, AirDivisionId = 2, Year = 2024, WorkType = "firePatrol", SubType = "inspection", Hours = 40.0, DaysWithFlights = 0 },
        new FlightHour { Id = 8, AirDivisionId = 3, Year = 2024, WorkType = "firePatrol", SubType = "total", Hours = 280.0, DaysWithFlights = 38 },
        new FlightHour { Id = 9, AirDivisionId = 3, Year = 2024, WorkType = "firePatrol", SubType = "patrolDetected", Hours = 150.0, DaysWithFlights = 0 },
        new FlightHour { Id = 10, AirDivisionId = 4, Year = 2024, WorkType = "firePatrol", SubType = "total", Hours = 610.0, DaysWithFlights = 70 },
        new FlightHour { Id = 11, AirDivisionId = 4, Year = 2024, WorkType = "firePatrol", SubType = "patrolDetected", Hours = 400.0, DaysWithFlights = 0 },
        new FlightHour { Id = 12, AirDivisionId = 1, Year = 2025, WorkType = "firePatrol", SubType = "total", Hours = 390.0, DaysWithFlights = 55 },
        new FlightHour { Id = 13, AirDivisionId = 1, Year = 2025, WorkType = "firePatrol", SubType = "patrolDetected", Hours = 240.0, DaysWithFlights = 0 },
        new FlightHour { Id = 14, AirDivisionId = 3, Year = 2025, WorkType = "firePatrol", SubType = "total", Hours = 300.0, DaysWithFlights = 40 },
        new FlightHour { Id = 15, AirDivisionId = 3, Year = 2025, WorkType = "firePatrol", SubType = "patrolDetected", Hours = 170.0, DaysWithFlights = 0 }
    };

    protected override void Up(MigrationBuilder m)
    {
        var ctxOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(GetConnStr())
            .Options;

        using var db = new AppDbContext(ctxOptions);

        db.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Regions\", \"AirDivisions\", \"Forestries\", \"Fires\", \"FlightHours\", \"FormConfigs\", \"ValidationRules\" RESTART IDENTITY CASCADE");

        db.Regions.AddRange(RegionsSeed());
        db.AirDivisions.AddRange(DivisionsSeed());
        db.Forestries.AddRange(ForestriesSeed());
        db.Fires.AddRange(FiresSeed());
        db.FlightHours.AddRange(FlightHoursSeed());

        db.FormConfigs.AddRange(
            new FormConfigVersion { FormKey = "form-1-avia", Version = 1, EffectiveFromYear = 2024, ConfigJson = Form1V2024, IsActive = true, IsArchived = false },
            new FormConfigVersion { FormKey = "form-1-avia", Version = 2, EffectiveFromYear = 2026, ConfigJson = Form1V2026, IsActive = true, IsArchived = false },
            new FormConfigVersion { FormKey = "form-2-avia", Version = 1, EffectiveFromYear = 2024, ConfigJson = Form2V2024, IsActive = true, IsArchived = false },
            new FormConfigVersion { FormKey = "form-3-fires", Version = 1, EffectiveFromYear = 2024, ConfigJson = Form3V2024, IsActive = true, IsArchived = false }
        );

        db.ValidationRules.AddRange(Rules());
        db.SaveChanges();
    }

    protected override void Down(MigrationBuilder m)
    {
        var ctxOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(GetConnStr())
            .Options;
        using var db = new AppDbContext(ctxOptions);
        db.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Regions\", \"AirDivisions\", \"Forestries\", \"Fires\", \"FlightHours\", \"FormConfigs\", \"ValidationRules\" RESTART IDENTITY CASCADE");
    }

    static string GetConnStr()
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var cfgPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!File.Exists(cfgPath)) cfgPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        using var doc = System.Text.Json.JsonDocument.Parse(File.ReadAllText(cfgPath));
        return doc.RootElement.GetProperty("ConnectionStrings").GetProperty("DefaultConnection").GetString()!;
    }
}
