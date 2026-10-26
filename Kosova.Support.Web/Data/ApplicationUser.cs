using Microsoft.AspNetCore.Identity;

namespace Kosova.Support.Web.Data;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public string PreferredLanguage { get; set; } = "sq";
}
