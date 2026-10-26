using Kosova.Support.Infrastructure.Data;
using Kosova.Support.Web.Models.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Controllers;

public class ContentController(KosovaSupportDbContext dbContext) : Controller
{
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), HttpOnly = true, Secure = true });

        return LocalRedirect(returnUrl);
    }

    public IActionResult About() => View("SimplePage", ("About Kosova.Support", "Kosova.Support is a prototype public service portal for Kosovo scenarios."));
    public IActionResult OnlineApplications() => View("SimplePage", ("Online Applications", "Submit and track multi-step applications with draft saving."));
    public IActionResult Documents() => View("SimplePage", ("Documents and Certificates", "Upload and monitor document verification statuses."));
    public IActionResult Privacy() => View("SimplePage", ("Privacy Policy", "Data processing follows demonstration-only privacy controls and secure defaults."));
    public IActionResult Terms() => View("SimplePage", ("Terms of Use", "This prototype provides demo-only services and sandbox integrations."));
    public IActionResult Accessibility() => View("SimplePage", ("Accessibility Statement", "WCAG 2.2 AA design goals are implemented across navigation, contrast, and forms."));
    public IActionResult HelpCenter() => View("SimplePage", ("Help Center", "Browse support guidance and submit tickets for help."));
    public IActionResult Tenders() => View("SimplePage", ("Public Tenders and Notices", "Public notices and tender summaries are listed here."));
    public IActionResult Emergency() => View("SimplePage", ("Emergency Alerts", "Live emergency announcements are displayed with severity labels."));

    public async Task<IActionResult> News()
    {
        var items = await dbContext.NewsArticles.Where(x => x.IsPublished).OrderByDescending(x => x.PublishedAtUtc).ToListAsync();
        return View(items);
    }

    public async Task<IActionResult> Events()
    {
        var items = await dbContext.Events.OrderBy(x => x.StartsAtUtc).ToListAsync();
        return View(items);
    }

    public IActionResult SystemStatus() => View();

    [HttpGet]
    public IActionResult Contact() => View(new ContactFormViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(ContactFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        dbContext.SupportTickets.Add(new Kosova.Support.Core.Entities.SupportTicket
        {
            Subject = model.Subject,
            Description = $"{model.FullName} ({model.Email})\n\n{model.Message}",
            UserId = User.Identity?.Name ?? "public"
        });

        await dbContext.SaveChangesAsync();
        TempData["Success"] = "Your support ticket was submitted.";
        return RedirectToAction(nameof(Contact));
    }

    public async Task<IActionResult> FAQ()
    {
        var faqs = await dbContext.FAQs.OrderBy(x => x.SortOrder).ToListAsync();
        return View(faqs);
    }
}
