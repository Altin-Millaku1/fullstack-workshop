using Kosova.Support.Web.Models;

namespace Kosova.Support.Web.ViewModels;

public class HomeIndexViewModel
{
    public List<Service> PopularServices { get; set; } = [];
    public List<ServiceCategory> Categories { get; set; } = [];
    public List<NewsArticle> News { get; set; } = [];
    public List<Event> Events { get; set; } = [];
    public EmergencyAlert? ActiveAlert { get; set; }
}
