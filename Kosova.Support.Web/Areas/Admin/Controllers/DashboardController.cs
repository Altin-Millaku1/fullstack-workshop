using Kosova.Support.Web.Data;
using Kosova.Support.Web.Models;
using Kosova.Support.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,ServiceAdmin")]
public class DashboardController(ApplicationDbContext db, ApplicationWorkflowService workflowService) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewBag.Users = await db.Users.CountAsync();
        ViewBag.Applications = await db.Applications.CountAsync();
        ViewBag.PendingApplications = await db.Applications.CountAsync(x => x.Status == ApplicationStatus.Submitted || x.Status == ApplicationStatus.InReview);
        ViewBag.Payments = await db.Payments.CountAsync(x => x.Status == PaymentStatus.Completed);
        ViewBag.SupportTickets = await db.SupportTickets.CountAsync(x => x.Status == "Open");
        ViewBag.RecentApplications = await db.Applications.Include(x => x.Service).OrderByDescending(x => x.Id).Take(10).ToListAsync();
        return View();
    }

    public async Task<IActionResult> Services() => View(await db.Services.Include(x => x.ServiceCategory).Include(x => x.Department).ToListAsync());
    public async Task<IActionResult> Departments() => View(await db.Departments.ToListAsync());
    public async Task<IActionResult> Users() => View(await db.Users.OrderBy(x => x.Email).ToListAsync());
    public async Task<IActionResult> Applications() => View(await db.Applications.Include(x => x.Service).Include(x => x.User).OrderByDescending(x => x.Id).ToListAsync());
    public async Task<IActionResult> Appointments() => View(await db.Appointments.Include(x => x.Service).OrderByDescending(x => x.AppointmentTime).ToListAsync());
    public async Task<IActionResult> Payments() => View(await db.Payments.Include(x => x.GovernmentApplication).OrderByDescending(x => x.CreatedAt).ToListAsync());
    public async Task<IActionResult> News() => View(await db.NewsArticles.OrderByDescending(x => x.PublishedAt).ToListAsync());
    public async Task<IActionResult> Events() => View(await db.Events.OrderByDescending(x => x.EventDate).ToListAsync());
    public async Task<IActionResult> FAQs() => View(await db.FAQs.ToListAsync());
    public async Task<IActionResult> Alerts() => View(await db.EmergencyAlerts.OrderByDescending(x => x.StartsAt).ToListAsync());
    public async Task<IActionResult> AuditLogs() => View(await db.AuditLogs.OrderByDescending(x => x.CreatedAt).Take(100).ToListAsync());
    public async Task<IActionResult> Settings() => View(await db.SystemSettings.ToListAsync());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(int id)
    {
        await workflowService.SetStatusAsync(id, ApplicationStatus.Approved, User.Identity?.Name ?? "admin", "Application approved.");
        return RedirectToAction(nameof(Applications));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reject(int id)
    {
        await workflowService.SetStatusAsync(id, ApplicationStatus.Rejected, User.Identity?.Name ?? "admin", "Application rejected.");
        return RedirectToAction(nameof(Applications));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RequestInfo(int id)
    {
        await workflowService.SetStatusAsync(id, ApplicationStatus.AdditionalInformationRequested, User.Identity?.Name ?? "admin", "Additional information requested.");
        return RedirectToAction(nameof(Applications));
    }
}
