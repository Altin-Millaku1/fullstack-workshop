using Kosova.Support.Core.Entities;
using Kosova.Support.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Infrastructure.Services;

public class AuditService(KosovaSupportDbContext dbContext)
{
    public async Task LogAsync(string userId, string action, string entityName, string entityId, string metadataJson = "{}")
    {
        dbContext.AuditLogs.Add(new AuditLog
        {
            UserId = userId,
            Action = action,
            EntityName = entityName,
            EntityId = entityId,
            MetadataJson = metadataJson
        });

        await dbContext.SaveChangesAsync();
    }

    public Task<List<AuditLog>> RecentAsync() => dbContext.AuditLogs.OrderByDescending(x => x.CreatedAtUtc).Take(100).ToListAsync();
}
