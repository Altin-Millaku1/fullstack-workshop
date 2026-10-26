using System.ComponentModel.DataAnnotations;

namespace Kosova.Support.Web.Models.ViewModels;

public class ApplicationStepViewModel
{
    public int ApplicationId { get; set; }
    public int StepNumber { get; set; }

    [Required]
    [StringLength(500)]
    public string ApplicantStatement { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string ContactEmail { get; set; } = string.Empty;
}
