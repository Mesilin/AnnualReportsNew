using Microsoft.AspNetCore.Mvc;
using YasenReportsDemo.Services;

namespace YasenReportsDemo.Controllers;

public class ReportController : Controller
{
    private readonly ReportService _svc;
    public ReportController(ReportService svc) => _svc = svc;

    /// <summary>Открыть форму: formKey/year/region. Текущий год — создаёт и позволяет вводить; прошлый — только чтение snapshot.</summary>
    [HttpGet]
    public async Task<IActionResult> Open(string formKey, int year, int regionId, bool recalc = true)
    {
        try
        {
            var vm = await _svc.GetOrCreateAsync(formKey, year, regionId, recalc);
            if (vm is null)
            {
                TempData["Msg"] = $"Отчёт «{formKey}» за {year} г. не найден (за прошлые годы отчёты только читаются, создавать можно только за текущий {_svc.CurrentYear} г.).";
                return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }
        catch (InvalidOperationException ex)
        {
            TempData["Msg"] = ex.Message;
            return RedirectToAction("Index", "Home");
        }
    }

    /// <summary>AJAX-сохранение строки + валидация.</summary>
    [HttpPost]
    public async Task<IActionResult> SaveRow(int reportId, string rowKey, Dictionary<string, double?> values)
    {
        try
        {
            var (ok, errors) = await _svc.SaveRowAsync(reportId, rowKey, values ?? new());
            return Json(new { ok, errors });
        }
        catch (InvalidOperationException ex)
        {
            return Json(new { ok = false, errors = new[] { new { message = ex.Message } } });
        }
    }

    /// <summary>Финализация (заморозка) отчёта.</summary>
    [HttpPost]
    public async Task<IActionResult> Finalize(int reportId)
    {
        try
        {
            await _svc.FinalizeAsync(reportId);
            TempData["Msg"] = "Отчёт утверждён. Данные зафиксированы, дальнейший пересчёт из источников не производится.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Msg"] = ex.Message;
        }
        return RedirectToAction(nameof(Index));
    }

    /// <summary>Список созданных отчётов.</summary>
    [HttpGet]
    public IActionResult Index() => RedirectToAction("Index", "Home");
}
