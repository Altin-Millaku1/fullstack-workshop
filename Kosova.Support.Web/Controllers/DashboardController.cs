using System.Security.Claims;
using Kosova.Support.Infrastructure.Data;
using Kosova.Support.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Controllers;

[Authorize]
public class DashboardController(KosovaSupportDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var model = new DashboardViewModel
        {
            Applications = await dbContext.Applications.Include(x => x.Service).Where(x => x.UserId == userId).ToListAsync(),
            Appointments = await dbContext.Appointments.Include(x => x.Service).Where(x => x.UserId == userId).ToListAsync(),
            Notifications = await dbContext.Notifications.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedAtUtc).Take(10).ToListAsync(),
            Payments = await dbContext.Payments.Include(x => x.Application).Where(x => x.Application!.UserId == userId).ToListAsync()
        };

        return View(model);
    }
}
