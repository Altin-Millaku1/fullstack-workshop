using System.ComponentModel.DataAnnotations;

namespace Kosova.Support.Web.Models.ViewModels;

public class ContactFormViewModel
{
    [Required]
    [StringLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(120)]
    public string Subject { get; set; } = string.Empty;

    [Required]
    [StringLength(3000)]
    public string Message { get; set; } = string.Empty;
}
