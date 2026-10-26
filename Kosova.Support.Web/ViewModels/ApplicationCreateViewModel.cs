using System.ComponentModel.DataAnnotations;

namespace Kosova.Support.Web.ViewModels;

public class ApplicationCreateViewModel
{
    [Required]
    public int ServiceId { get; set; }

    [Required]
    [MaxLength(200)]
    public string ApplicantName { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Purpose { get; set; } = string.Empty;

    [MaxLength(500)]
    public string AdditionalInformation { get; set; } = string.Empty;

    public bool SaveAsDraft { get; set; }
}
