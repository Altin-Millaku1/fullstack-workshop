using Kosova.Support.Web.Data;
using Kosova.Support.Web.Interfaces;
using Kosova.Support.Web.Models;

namespace Kosova.Support.Web.Services;

public class AuditService(ApplicationDbContext db) : IAuditService
{
    public async Task LogAsync(string action, string entityName, string entityId, string userId, string metadata = "")
    {
        db.AuditLogs.Add(new AuditLog
        {
            Action = action,
            EntityName = entityName,
            EntityId = entityId,
            UserId = userId,
            Metadata = metadata,
            CreatedAt = DateTime.UtcNow
        });
        await db.SaveChangesAsync();
    }
}
