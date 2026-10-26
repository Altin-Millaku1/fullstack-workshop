using Kosova.Support.Web.Data;
using Kosova.Support.Web.Interfaces;
using Kosova.Support.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Services;

public class ApplicationWorkflowService(
    ApplicationDbContext db,
    NotificationService notifications,
    IAuditService auditService)
{
    public async Task<GovernmentApplication> SaveDraftAsync(string userId, int serviceId, string formDataJson)
    {
        var app = new GovernmentApplication
        {
            UserId = userId,
            ServiceId = serviceId,
            FormDataJson = formDataJson,
            Status = ApplicationStatus.Draft,
            ReferenceNumber = $"KS-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..8]}"
        };

        db.Applications.Add(app);
        await db.SaveChangesAsync();
        await auditService.LogAsync("DraftSaved", nameof(GovernmentApplication), app.Id.ToString(), userId);
        return app;
    }

    public async Task SubmitAsync(int applicationId, string userId)
    {
        var app = await db.Applications.FirstAsync(x => x.Id == applicationId && x.UserId == userId);
        app.Status = ApplicationStatus.Submitted;
        app.SubmittedAt = DateTime.UtcNow;
        await db.SaveChangesAsync();

        await notifications.CreateAsync(userId, "Application submitted", $"Application {app.ReferenceNumber} submitted.");
        await auditService.LogAsync("Submitted", nameof(GovernmentApplication), app.Id.ToString(), userId);
    }

    public async Task SetStatusAsync(int applicationId, ApplicationStatus status, string adminUserId, string note)
    {
        var app = await db.Applications.Include(x => x.User).FirstAsync(x => x.Id == applicationId);
        app.Status = status;
        await db.SaveChangesAsync();
        await notifications.CreateAsync(app.UserId, "Application update", note);
        await auditService.LogAsync($"Status:{status}", nameof(GovernmentApplication), app.Id.ToString(), adminUserId, note);
    }
}
