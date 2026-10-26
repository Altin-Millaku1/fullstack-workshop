using System.ComponentModel.DataAnnotations;

namespace Kosova.Support.Web.ViewModels;

public class SupportTicketViewModel
{
    [Required, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Subject { get; set; } = string.Empty;

    [Required, MaxLength(3000)]
    public string Message { get; set; } = string.Empty;
}
