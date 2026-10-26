using System.Security.Claims;
using Kosova.Support.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Controllers;

[Authorize]
public class DashboardController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        ViewBag.Applications = await db.Applications.Include(x => x.Service).Where(x => x.UserId == userId).OrderByDescending(x => x.Id).ToListAsync();
        ViewBag.Notifications = await db.Notifications.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedAt).Take(10).ToListAsync();
        ViewBag.Payments = await db.Payments.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedAt).Take(10).ToListAsync();
        return View();
    }
}
