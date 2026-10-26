using System.ComponentModel.DataAnnotations;

namespace Kosova.Support.Web.ViewModels;

public class BookAppointmentViewModel
{
    [Required]
    public int ServiceId { get; set; }

    [Required]
    public DateTime AppointmentTime { get; set; }

    [Required]
    [MaxLength(250)]
    public string Location { get; set; } = string.Empty;
}
