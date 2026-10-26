using Microsoft.AspNetCore.Identity;

namespace Kosova.Support.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public bool IsGovernmentEmployee { get; set; }
}
