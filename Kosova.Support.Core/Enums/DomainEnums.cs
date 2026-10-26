namespace Kosova.Support.Core.Enums;

public enum ApplicationStatus
{
    Draft,
    Submitted,
    UnderReview,
    RequiresMoreInfo,
    Approved,
    Rejected
}

public enum DocumentStatus
{
    Pending,
    Accepted,
    Rejected
}

public enum PaymentStatus
{
    Pending,
    Authorized,
    Completed,
    Failed,
    Refunded
}

public enum AppointmentStatus
{
    Scheduled,
    Cancelled,
    Completed
}

public enum NotificationType
{
    InApp,
    Email,
    System
}
