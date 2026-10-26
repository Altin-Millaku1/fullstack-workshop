namespace Kosova.Support.Web.Interfaces;

public interface IAuditService
{
    Task LogAsync(string action, string entityName, string entityId, string userId, string metadata = "");
}
