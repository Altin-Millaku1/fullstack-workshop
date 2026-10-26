using Kosova.Support.Core.Entities;
using Kosova.Support.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Infrastructure.Data;

public class KosovaSupportDbContext(DbContextOptions<KosovaSupportDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<CitizenProfile> CitizenProfiles => Set<CitizenProfile>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<ApplicationStep> ApplicationSteps => Set<ApplicationStep>();
    public DbSet<ApplicationDocument> ApplicationDocuments => Set<ApplicationDocument>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<NewsArticle> NewsArticles => Set<NewsArticle>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<EmergencyAlert> EmergencyAlerts => Set<EmergencyAlert>();
    public DbSet<FAQ> FAQs => Set<FAQ>();
    public DbSet<SupportTicket> SupportTickets => Set<SupportTicket>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<Translation> Translations => Set<Translation>();
    public DbSet<SystemSetting> SystemSettings => Set<SystemSetting>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Service>().HasIndex(x => x.Slug).IsUnique();
        builder.Entity<ServiceCategory>().HasIndex(x => x.Slug).IsUnique();
        builder.Entity<Translation>().HasIndex(x => new { x.Culture, x.ResourceKey }).IsUnique();
        builder.Entity<SystemSetting>().HasIndex(x => x.Key).IsUnique();

        builder.Entity<ApplicationStep>()
            .HasOne(x => x.Application)
            .WithMany(x => x.Steps)
            .HasForeignKey(x => x.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ApplicationDocument>()
            .HasOne(x => x.Application)
            .WithMany(x => x.Documents)
            .HasForeignKey(x => x.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
