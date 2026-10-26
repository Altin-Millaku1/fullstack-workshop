using Kosova.Support.Infrastructure.Data;
using Kosova.Support.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController(KosovaSupportDbContext dbContext, AuditService auditService) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewBag.Users = await dbContext.Users.CountAsync();
        ViewBag.Services = await dbContext.Services.CountAsync();
        ViewBag.Applications = await dbContext.Applications.CountAsync();
        ViewBag.PendingApplications = await dbContext.Applications.CountAsync(x => x.Status == Kosova.Support.Core.Enums.ApplicationStatus.Submitted);
        ViewBag.Payments = await dbContext.Payments.CountAsync();
        ViewBag.Tickets = await dbContext.SupportTickets.CountAsync();
        ViewBag.Audit = await auditService.RecentAsync();
        return View();
    }
}
