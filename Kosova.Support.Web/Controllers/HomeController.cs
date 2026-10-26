using Kosova.Support.Infrastructure.Data;
using Kosova.Support.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Controllers;

public class HomeController(KosovaSupportDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel
        {
            Alert = await dbContext.EmergencyAlerts.FirstOrDefaultAsync(x => x.IsActive),
            PopularServices = await dbContext.Services.Include(x => x.ServiceCategory).Take(6).ToListAsync(),
            Categories = await dbContext.ServiceCategories.Include(x => x.Services).ToListAsync(),
            News = await dbContext.NewsArticles.Where(x => x.IsPublished).OrderByDescending(x => x.PublishedAtUtc).Take(4).ToListAsync(),
            Events = await dbContext.Events.OrderBy(x => x.StartsAtUtc).Take(4).ToListAsync()
        };

        return View(model);
    }

    public IActionResult Error() => View();
}
