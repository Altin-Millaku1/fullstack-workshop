using Kosova.Support.Core.Entities;

namespace Kosova.Support.Web.Models.ViewModels;

public class DashboardViewModel
{
    public List<Application> Applications { get; set; } = [];
    public List<Appointment> Appointments { get; set; } = [];
    public List<Notification> Notifications { get; set; } = [];
    public List<Payment> Payments { get; set; } = [];
}
