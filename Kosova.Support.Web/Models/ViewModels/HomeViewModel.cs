using Kosova.Support.Core.Entities;

namespace Kosova.Support.Web.Models.ViewModels;

public class HomeViewModel
{
    public EmergencyAlert? Alert { get; set; }
    public List<Service> PopularServices { get; set; } = [];
    public List<ServiceCategory> Categories { get; set; } = [];
    public List<NewsArticle> News { get; set; } = [];
    public List<Event> Events { get; set; } = [];
}
