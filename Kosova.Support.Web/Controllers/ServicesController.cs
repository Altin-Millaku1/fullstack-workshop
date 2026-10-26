using Kosova.Support.Infrastructure.Data;
using Kosova.Support.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Controllers;

public class ServicesController(KosovaSupportDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index(string? query, int? categoryId)
    {
        var servicesQuery = dbContext.Services.Include(x => x.ServiceCategory).Include(x => x.Department).AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            servicesQuery = servicesQuery.Where(x => x.Name.Contains(query) || x.Summary.Contains(query));
        }

        if (categoryId.HasValue)
        {
            servicesQuery = servicesQuery.Where(x => x.ServiceCategoryId == categoryId.Value);
        }

        var model = new ServiceSearchViewModel
        {
            Query = query,
            CategoryId = categoryId,
            Categories = await dbContext.ServiceCategories.OrderBy(x => x.Name).ToListAsync(),
            Results = await servicesQuery.OrderBy(x => x.Name).ToListAsync()
        };

        return View(model);
    }

    [HttpGet("services/{slug}")]
    public async Task<IActionResult> Details(string slug)
    {
        var service = await dbContext.Services
            .Include(x => x.Department)
            .Include(x => x.ServiceCategory)
            .FirstOrDefaultAsync(x => x.Slug == slug);

        return service is null ? NotFound() : View(service);
    }
}
