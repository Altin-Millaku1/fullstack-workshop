using System.Security.Claims;
using Kosova.Support.Web.Data;
using Kosova.Support.Web.Interfaces;
using Kosova.Support.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Controllers;

[Authorize]
public class DocumentsController(ApplicationDbContext db, IFileValidationService fileValidationService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var items = await db.ApplicationDocuments
            .Include(x => x.GovernmentApplication)
            .ThenInclude(x => x!.Service)
            .Where(x => x.GovernmentApplication!.UserId == userId)
            .OrderByDescending(x => x.UploadedAt)
            .ToListAsync();
        return View(items);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(int applicationId, IFormFile file)
    {
        var validation = await fileValidationService.ValidateAsync(file);
        if (!validation.IsValid)
        {
            TempData["Error"] = validation.ErrorMessage;
            return RedirectToAction(nameof(Index));
        }

        var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        Directory.CreateDirectory(uploadsPath);
        var safeName = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName).ToLowerInvariant()}";
        var fullPath = Path.Combine(uploadsPath, safeName);

        await using (var stream = System.IO.File.Create(fullPath))
        {
            await file.CopyToAsync(stream);
        }

        db.ApplicationDocuments.Add(new ApplicationDocument
        {
            GovernmentApplicationId = applicationId,
            FileName = file.FileName,
            StoredPath = $"uploads/{safeName}",
            SizeBytes = file.Length,
            Status = DocumentStatus.Pending
        });

        await db.SaveChangesAsync();
        TempData["Success"] = "Document uploaded.";
        return RedirectToAction(nameof(Index));
    }
}
