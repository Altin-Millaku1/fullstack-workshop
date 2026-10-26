using Kosova.Support.Core.Enums;

namespace Kosova.Support.Core.Entities;

public class CitizenProfile : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public string PersonalNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Municipality { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
}

public class Department : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<Service> Services { get; set; } = new List<Service>();
}

public class ServiceCategory : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<Service> Services { get; set; } = new List<Service>();
}

public class Service : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Eligibility { get; set; } = string.Empty;
    public string RequiredDocuments { get; set; } = string.Empty;
    public decimal FeeAmount { get; set; }
    public int ServiceCategoryId { get; set; }
    public ServiceCategory? ServiceCategory { get; set; }
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }
}

public class Application : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public int ServiceId { get; set; }
    public Service? Service { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Draft;
    public int CurrentStep { get; set; } = 1;
    public string Notes { get; set; } = string.Empty;
    public ICollection<ApplicationStep> Steps { get; set; } = new List<ApplicationStep>();
    public ICollection<ApplicationDocument> Documents { get; set; } = new List<ApplicationDocument>();
}

public class ApplicationStep : BaseEntity
{
    public int ApplicationId { get; set; }
    public Application? Application { get; set; }
    public int StepNumber { get; set; }
    public string DataJson { get; set; } = "{}";
    public bool IsComplete { get; set; }
}

public class ApplicationDocument : BaseEntity
{
    public int ApplicationId { get; set; }
    public Application? Application { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string StoragePath { get; set; } = string.Empty;
    public long SizeBytes { get; set; }
    public DocumentStatus Status { get; set; } = DocumentStatus.Pending;
}

public class Appointment : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public int ServiceId { get; set; }
    public Service? Service { get; set; }
    public DateTime ScheduledAtUtc { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
}

public class Payment : BaseEntity
{
    public int ApplicationId { get; set; }
    public Application? Application { get; set; }
    public string ExternalReference { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string Currency { get; set; } = "EUR";
}

public class Notification : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; } = NotificationType.InApp;
    public bool IsRead { get; set; }
}

public class NewsArticle : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public DateTime? PublishedAtUtc { get; set; }
}

public class Event : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartsAtUtc { get; set; }
    public string Location { get; set; } = string.Empty;
}

public class EmergencyAlert : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string Severity { get; set; } = "Info";
}

public class FAQ : BaseEntity
{
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}

public class SupportTicket : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Open";
}

public class AuditLog : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;
    public string MetadataJson { get; set; } = "{}";
}

public class Translation : BaseEntity
{
    public string Culture { get; set; } = string.Empty;
    public string ResourceKey { get; set; } = string.Empty;
    public string ResourceValue { get; set; } = string.Empty;
}

public class SystemSetting : BaseEntity
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
