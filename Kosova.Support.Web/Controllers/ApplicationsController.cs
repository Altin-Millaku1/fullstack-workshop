using System.Security.Claims;
using System.Text.Json;
using Kosova.Support.Web.Data;
using Kosova.Support.Web.Models;
using Kosova.Support.Web.Services;
using Kosova.Support.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Controllers;

[Authorize]
public class ApplicationsController(ApplicationDbContext db, ApplicationWorkflowService workflowService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var items = await db.Applications.Include(x => x.Service).Where(x => x.UserId == userId).OrderByDescending(x => x.Id).ToListAsync();
        return View(items);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Services = await db.Services.Where(x => x.IsActive).OrderBy(x => x.Title).ToListAsync();
        return View(new ApplicationCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ApplicationCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Services = await db.Services.Where(x => x.IsActive).OrderBy(x => x.Title).ToListAsync();
            return View(model);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var payload = JsonSerializer.Serialize(new
        {
            model.ApplicantName,
            model.Purpose,
            model.AdditionalInformation,
            Steps = new[] { "Applicant", "Purpose", "Review" }
        });

        var app = await workflowService.SaveDraftAsync(userId, model.ServiceId, payload);

        if (!model.SaveAsDraft)
        {
            await workflowService.SubmitAsync(app.Id, userId);
        }

        TempData["Success"] = model.SaveAsDraft ? "Draft saved." : "Application submitted.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Submit(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        await workflowService.SubmitAsync(id, userId);
        TempData["Success"] = "Application submitted.";
        return RedirectToAction(nameof(Index));
    }
}
