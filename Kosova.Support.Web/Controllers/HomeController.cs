using Kosova.Support.Web.Data;
using Kosova.Support.Web.Models;
using Kosova.Support.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Controllers;

public class HomeController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var model = new HomeIndexViewModel
        {
            PopularServices = await db.Services.Where(x => x.IsActive).Take(6).ToListAsync(),
            Categories = await db.ServiceCategories.Include(x => x.Services).ToListAsync(),
            News = await db.NewsArticles.OrderByDescending(x => x.PublishedAt).Take(5).ToListAsync(),
            Events = await db.Events.OrderBy(x => x.EventDate).Take(5).ToListAsync(),
            ActiveAlert = await db.EmergencyAlerts.FirstOrDefaultAsync(x => x.IsActive && x.EndsAt >= DateTime.UtcNow)
        };

        return View(model);
    }

    public async Task<IActionResult> About() => View(new ContentPageViewModel
    {
        Title = "About Kosova.Support",
        Description = "Kosova.Support is a prototype and demonstration platform for public-service digitization in Kosovo."
    });

    public async Task<IActionResult> GovernmentServices(string? search)
    {
        var query = db.Services.Include(x => x.ServiceCategory).Include(x => x.Department).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x => x.Title.Contains(search) || x.TitleEn.Contains(search) || x.TitleSr.Contains(search));
        }

        ViewBag.Search = search;
        return View(await query.OrderBy(x => x.Title).ToListAsync());
    }

    public async Task<IActionResult> ServiceDetails(int id)
    {
        var service = await db.Services.Include(x => x.Department).Include(x => x.ServiceCategory).FirstOrDefaultAsync(x => x.Id == id);
        return service is null ? NotFound() : View(service);
    }

    public IActionResult OnlineApplications() => View();
    public async Task<IActionResult> News() => View(await db.NewsArticles.OrderByDescending(x => x.PublishedAt).ToListAsync());
    public async Task<IActionResult> Events() => View(await db.Events.OrderBy(x => x.EventDate).ToListAsync());
    public async Task<IActionResult> EmergencyAlerts() => View(await db.EmergencyAlerts.OrderByDescending(x => x.StartsAt).ToListAsync());
    public async Task<IActionResult> PublicTenders() => View(await db.PublicTenders.OrderBy(x => x.Deadline).ToListAsync());
    public async Task<IActionResult> FAQ() => View(await db.FAQs.ToListAsync());
    public IActionResult HelpCenter() => View();
    public IActionResult Privacy() => View();
    public IActionResult Terms() => View();
    public IActionResult Accessibility() => View();

    public IActionResult Contact() => View(new SupportTicketViewModel());

    [HttpPost]
    [EnableRateLimiting("PublicForms")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(SupportTicketViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        db.SupportTickets.Add(new SupportTicket
        {
            Name = model.Name,
            Email = model.Email,
            Subject = model.Subject,
            Message = model.Message,
            Status = "Open"
        });
        await db.SaveChangesAsync();
        TempData["Success"] = "Support ticket created successfully.";
        return RedirectToAction(nameof(Contact));
    }

    public async Task<IActionResult> SystemStatus()
    {
        ViewBag.TotalServices = await db.Services.CountAsync();
        ViewBag.ActiveAlerts = await db.EmergencyAlerts.CountAsync(x => x.IsActive);
        ViewBag.OpenTickets = await db.SupportTickets.CountAsync(x => x.Status == "Open");
        return View();
    }
}
