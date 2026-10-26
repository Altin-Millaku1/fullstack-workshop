using Kosova.Support.Core.Entities;
using Kosova.Support.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ManagementController(KosovaSupportDbContext dbContext) : Controller
{
    public async Task<IActionResult> Services() => View(await dbContext.Services.Include(x => x.ServiceCategory).Include(x => x.Department).ToListAsync());
    public async Task<IActionResult> Departments() => View(await dbContext.Departments.ToListAsync());
    public async Task<IActionResult> Categories() => View(await dbContext.ServiceCategories.ToListAsync());
    public async Task<IActionResult> News() => View(await dbContext.NewsArticles.ToListAsync());
    public async Task<IActionResult> Alerts() => View(await dbContext.EmergencyAlerts.ToListAsync());
    public async Task<IActionResult> Faqs() => View(await dbContext.FAQs.OrderBy(x => x.SortOrder).ToListAsync());
    public async Task<IActionResult> Appointments() => View(await dbContext.Appointments.Include(x => x.Service).ToListAsync());
    public async Task<IActionResult> Payments() => View(await dbContext.Payments.Include(x => x.Application).ToListAsync());
    public async Task<IActionResult> Applications() => View(await dbContext.Applications.Include(x => x.Service).ToListAsync());
    public async Task<IActionResult> Tickets() => View(await dbContext.SupportTickets.ToListAsync());
    public async Task<IActionResult> Translations() => View(await dbContext.Translations.OrderBy(x => x.Culture).ToListAsync());
    public async Task<IActionResult> Settings() => View(await dbContext.SystemSettings.OrderBy(x => x.Key).ToListAsync());
    public async Task<IActionResult> Users() => View(await dbContext.Users.OrderBy(x => x.Email).ToListAsync());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddDepartment(string name, string description)
    {
        dbContext.Departments.Add(new Department { Name = name, Description = description });
        await dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Departments));
    }
}
