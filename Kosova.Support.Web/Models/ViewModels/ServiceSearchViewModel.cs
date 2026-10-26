using Kosova.Support.Core.Entities;

namespace Kosova.Support.Web.Models.ViewModels;

public class ServiceSearchViewModel
{
    public string? Query { get; set; }
    public int? CategoryId { get; set; }
    public List<ServiceCategory> Categories { get; set; } = [];
    public List<Service> Results { get; set; } = [];
}
