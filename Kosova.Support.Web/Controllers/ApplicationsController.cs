using System.Security.Claims;
using System.Text.Json;
using Kosova.Support.Core.Entities;
using Kosova.Support.Core.Enums;
using Kosova.Support.Core.Interfaces;
using Kosova.Support.Infrastructure.Data;
using Kosova.Support.Infrastructure.Services;
using Kosova.Support.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Controllers;

[Authorize]
public class ApplicationsController(KosovaSupportDbContext dbContext, IFileScanner fileScanner, AuditService auditService) : Controller
{
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Start(int serviceId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var application = new Application
        {
            UserId = userId,
            ServiceId = serviceId,
            Status = ApplicationStatus.Draft
        };

        dbContext.Applications.Add(application);
        await dbContext.SaveChangesAsync();
        await auditService.LogAsync(userId, "ApplicationStarted", nameof(Application), application.Id.ToString());
        return RedirectToAction(nameof(EditStep), new { id = application.Id, step = 1 });
    }

    public async Task<IActionResult> EditStep(int id, int step = 1)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var app = await dbContext.Applications.Include(x => x.Service).FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (app is null)
        {
            return NotFound();
        }

        var stepData = await dbContext.ApplicationSteps.FirstOrDefaultAsync(x => x.ApplicationId == id && x.StepNumber == step);
        var model = stepData is null
            ? new ApplicationStepViewModel { ApplicationId = id, StepNumber = step, ContactEmail = User.Identity?.Name ?? string.Empty }
            : JsonSerializer.Deserialize<ApplicationStepViewModel>(stepData.DataJson) ?? new ApplicationStepViewModel { ApplicationId = id, StepNumber = step };

        ViewBag.ServiceName = app.Service?.Name;
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveStep(ApplicationStepViewModel model, string actionType)
    {
        if (!ModelState.IsValid)
        {
            return View(nameof(EditStep), model);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var app = await dbContext.Applications.FirstOrDefaultAsync(x => x.Id == model.ApplicationId && x.UserId == userId);
        if (app is null)
        {
            return NotFound();
        }

        var step = await dbContext.ApplicationSteps.FirstOrDefaultAsync(x => x.ApplicationId == model.ApplicationId && x.StepNumber == model.StepNumber);
        if (step is null)
        {
            step = new ApplicationStep { ApplicationId = model.ApplicationId, StepNumber = model.StepNumber };
            dbContext.ApplicationSteps.Add(step);
        }

        step.DataJson = JsonSerializer.Serialize(model);
        step.IsComplete = true;
        app.CurrentStep = model.StepNumber;

        if (actionType == "submit")
        {
            app.Status = ApplicationStatus.Submitted;
        }

        await dbContext.SaveChangesAsync();
        await auditService.LogAsync(userId, "ApplicationStepSaved", nameof(Application), app.Id.ToString(), step.DataJson);

        if (actionType == "submit")
        {
            return RedirectToAction("Index", "Dashboard");
        }

        return RedirectToAction(nameof(EditStep), new { id = model.ApplicationId, step = model.StepNumber + 1 });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequestFormLimits(MultipartBodyLengthLimit = 5_000_000)]
    public async Task<IActionResult> UploadDocument(int applicationId, IFormFile file)
    {
        if (file.Length == 0 || file.Length > 5_000_000)
        {
            TempData["Error"] = "Invalid file size.";
            return RedirectToAction(nameof(EditStep), new { id = applicationId, step = 1 });
        }

        var allowedExtensions = new[] { ".pdf", ".png", ".jpg", ".jpeg" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(extension))
        {
            TempData["Error"] = "Only PDF and image files are accepted.";
            return RedirectToAction(nameof(EditStep), new { id = applicationId, step = 1 });
        }

        await using var stream = file.OpenReadStream();
        if (!await fileScanner.IsCleanAsync(stream, file.FileName))
        {
            TempData["Error"] = "Security scan failed.";
            return RedirectToAction(nameof(EditStep), new { id = applicationId, step = 1 });
        }

        var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        Directory.CreateDirectory(uploadDirectory);

        var fileName = $"{applicationId}_{Guid.NewGuid():N}{extension}";
        var storagePath = Path.Combine(uploadDirectory, fileName);

        await using (var outStream = System.IO.File.Create(storagePath))
        {
            await file.CopyToAsync(outStream);
        }

        dbContext.ApplicationDocuments.Add(new ApplicationDocument
        {
            ApplicationId = applicationId,
            FileName = file.FileName,
            StoragePath = $"/uploads/{fileName}",
            SizeBytes = file.Length,
            Status = DocumentStatus.Pending
        });

        await dbContext.SaveChangesAsync();
        TempData["Success"] = "Document uploaded.";
        return RedirectToAction(nameof(EditStep), new { id = applicationId, step = 1 });
    }
}
