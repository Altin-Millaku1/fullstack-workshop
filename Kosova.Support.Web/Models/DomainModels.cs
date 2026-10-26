using System.ComponentModel.DataAnnotations;
using Kosova.Support.Web.Data;

namespace Kosova.Support.Web.Models;

public enum ApplicationStatus
{
    Draft,
    Submitted,
    InReview,
    Approved,
    Rejected,
    AdditionalInformationRequested
}

public enum DocumentStatus
{
    Pending,
    Verified,
    Rejected
}

public enum PaymentStatus
{
    Pending,
    Completed,
    Failed,
    Refunded
}

public class CitizenProfile
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    [MaxLength(20)]
    public string PersonalNumber { get; set; } = string.Empty;
    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Municipality { get; set; } = string.Empty;
    [MaxLength(50)]
    public string ResidencyStatus { get; set; } = string.Empty;
    public ApplicationUser? User { get; set; }
}

public class Department
{
    public int Id { get; set; }
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(150)]
    public string NameEn { get; set; } = string.Empty;
    [MaxLength(150)]
    public string NameSr { get; set; } = string.Empty;
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;
    public ICollection<Service> Services { get; set; } = new List<Service>();
}

public class ServiceCategory
{
    public int Id { get; set; }
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(150)]
    public string NameEn { get; set; } = string.Empty;
    [MaxLength(150)]
    public string NameSr { get; set; } = string.Empty;
    public ICollection<Service> Services { get; set; } = new List<Service>();
}

public class Service
{
    public int Id { get; set; }
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(200)]
    public string TitleEn { get; set; } = string.Empty;
    [MaxLength(200)]
    public string TitleSr { get; set; } = string.Empty;
    [MaxLength(1500)]
    public string Description { get; set; } = string.Empty;
    [MaxLength(1500)]
    public string DescriptionEn { get; set; } = string.Empty;
    [MaxLength(1500)]
    public string DescriptionSr { get; set; } = string.Empty;
    public decimal Fee { get; set; }
    public int EstimatedDays { get; set; }
    public bool IsActive { get; set; } = true;
    public int ServiceCategoryId { get; set; }
    public int DepartmentId { get; set; }
    public ServiceCategory? ServiceCategory { get; set; }
    public Department? Department { get; set; }
    public ICollection<GovernmentApplication> Applications { get; set; } = new List<GovernmentApplication>();
}

public class GovernmentApplication
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int ServiceId { get; set; }
    [MaxLength(100)]
    public string ReferenceNumber { get; set; } = string.Empty;
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Draft;
    public DateTime SubmittedAt { get; set; }
    [MaxLength(3000)]
    public string FormDataJson { get; set; } = "{}";
    public ApplicationUser? User { get; set; }
    public Service? Service { get; set; }
    public ICollection<ApplicationStep> Steps { get; set; } = new List<ApplicationStep>();
    public ICollection<ApplicationDocument> Documents { get; set; } = new List<ApplicationDocument>();
}

public class ApplicationStep
{
    public int Id { get; set; }
    public int GovernmentApplicationId { get; set; }
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    public bool Completed { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public GovernmentApplication? GovernmentApplication { get; set; }
}

public class ApplicationDocument
{
    public int Id { get; set; }
    public int GovernmentApplicationId { get; set; }
    [MaxLength(260)]
    public string FileName { get; set; } = string.Empty;
    [MaxLength(260)]
    public string StoredPath { get; set; } = string.Empty;
    public long SizeBytes { get; set; }
    public DocumentStatus Status { get; set; } = DocumentStatus.Pending;
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public GovernmentApplication? GovernmentApplication { get; set; }
}

public class Appointment
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int ServiceId { get; set; }
    public DateTime AppointmentTime { get; set; }
    [MaxLength(250)]
    public string Location { get; set; } = string.Empty;
    [MaxLength(30)]
    public string Status { get; set; } = "Booked";
    public ApplicationUser? User { get; set; }
    public Service? Service { get; set; }
}

public class Payment
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int GovernmentApplicationId { get; set; }
    public decimal Amount { get; set; }
    [MaxLength(30)]
    public string Currency { get; set; } = "EUR";
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    [MaxLength(100)]
    public string ProviderReference { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? PaidAt { get; set; }
    public GovernmentApplication? GovernmentApplication { get; set; }
}

public class Notification
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(1000)]
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class NewsArticle
{
    public int Id { get; set; }
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(200)]
    public string TitleEn { get; set; } = string.Empty;
    [MaxLength(200)]
    public string TitleSr { get; set; } = string.Empty;
    [MaxLength(5000)]
    public string Content { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    public bool IsAnnouncement { get; set; }
}

public class Event
{
    public int Id { get; set; }
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    [MaxLength(200)]
    public string Location { get; set; } = string.Empty;
}

public class EmergencyAlert
{
    public int Id { get; set; }
    [MaxLength(250)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(1000)]
    public string Message { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
}

public class FAQ
{
    public int Id { get; set; }
    [MaxLength(300)]
    public string Question { get; set; } = string.Empty;
    [MaxLength(2000)]
    public string Answer { get; set; } = string.Empty;
}

public class SupportTicket
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(150)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [MaxLength(200)]
    public string Subject { get; set; } = string.Empty;
    [MaxLength(3000)]
    public string Message { get; set; } = string.Empty;
    [MaxLength(30)]
    public string Status { get; set; } = "Open";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class AuditLog
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Action { get; set; } = string.Empty;
    [MaxLength(100)]
    public string EntityName { get; set; } = string.Empty;
    [MaxLength(100)]
    public string EntityId { get; set; } = string.Empty;
    [MaxLength(100)]
    public string UserId { get; set; } = string.Empty;
    [MaxLength(1000)]
    public string Metadata { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class Translation
{
    public int Id { get; set; }
    [MaxLength(10)]
    public string LanguageCode { get; set; } = string.Empty;
    [MaxLength(200)]
    public string TranslationKey { get; set; } = string.Empty;
    [MaxLength(2000)]
    public string TranslationValue { get; set; } = string.Empty;
}

public class SystemSetting
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Key { get; set; } = string.Empty;
    [MaxLength(2000)]
    public string Value { get; set; } = string.Empty;
}

public class PublicTender
{
    public int Id { get; set; }
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
}
