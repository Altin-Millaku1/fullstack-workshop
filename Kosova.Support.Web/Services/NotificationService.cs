using Kosova.Support.Web.Data;
using Kosova.Support.Web.Models;

namespace Kosova.Support.Web.Services;

public class NotificationService(ApplicationDbContext db)
{
    public async Task CreateAsync(string userId, string title, string message)
    {
        db.Notifications.Add(new Notification
        {
            UserId = userId,
            Title = title,
            Message = message,
            CreatedAt = DateTime.UtcNow
        });
        await db.SaveChangesAsync();
    }
}
