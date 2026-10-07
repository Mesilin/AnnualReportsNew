using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YasenReportsDemo.Data;
using YasenReportsDemo.Services;

namespace YasenReportsDemo.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;
    private readonly FormConfigService _cfgSvc;
    private readonly ReportService _reportSvc;

    public HomeController(AppDbContext db, FormConfigService cfgSvc, ReportService reportSvc)
    {
        _db = db; _cfgSvc = cfgSvc; _reportSvc = reportSvc;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Forms = await _cfgSvc.ListAllActiveAsync();
        ViewBag.Regions = await _db.Regions.OrderBy(r => r.Name).ToListAsync();
        ViewBag.CurrentYear = _reportSvc.CurrentYear;
        return View();
    }

    /// <summary>Просмотр JSON-конфигов форм (для отладки механизма).</summary>
    public async Task<IActionResult> Configs()
    {
        var list = await _db.FormConfigs.Where(c => c.IsActive).OrderBy(c => c.FormKey).ThenByDescending(c => c.Version).ToListAsync();
        return Json(list.Select(c => new
        {
            c.FormKey,
            c.Version,
            c.EffectiveFromYear,
            Config = System.Text.Json.JsonDocument.Parse(c.ConfigJson).RootElement.ToString()
        }));
    }

    /// <summary>Список правил валидации из БД.</summary>
    public async Task<IActionResult> Rules()
    {
        var rules = await _db.ValidationRules.OrderBy(r => r.FormKey).ThenBy(r => r.SortOrder).ToListAsync();
        return View(rules);
    }
}
