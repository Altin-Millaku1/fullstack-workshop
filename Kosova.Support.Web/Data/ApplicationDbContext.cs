using Kosova.Support.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<CitizenProfile> CitizenProfiles => Set<CitizenProfile>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<GovernmentApplication> Applications => Set<GovernmentApplication>();
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
    public DbSet<PublicTender> PublicTenders => Set<PublicTender>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CitizenProfile>().HasIndex(x => x.UserId).IsUnique();
        builder.Entity<GovernmentApplication>().HasIndex(x => x.ReferenceNumber).IsUnique();
        builder.Entity<Translation>().HasIndex(x => new { x.LanguageCode, x.TranslationKey }).IsUnique();
        builder.Entity<SystemSetting>().HasIndex(x => x.Key).IsUnique();

        builder.Entity<GovernmentApplication>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<GovernmentApplication>()
            .HasOne(x => x.Service)
            .WithMany(x => x.Applications)
            .HasForeignKey(x => x.ServiceId);

        builder.Entity<ApplicationDocument>()
            .HasOne(x => x.GovernmentApplication)
            .WithMany(x => x.Documents)
            .HasForeignKey(x => x.GovernmentApplicationId);

        builder.Entity<ApplicationStep>()
            .HasOne(x => x.GovernmentApplication)
            .WithMany(x => x.Steps)
            .HasForeignKey(x => x.GovernmentApplicationId);

        builder.Entity<Appointment>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Payment>()
            .HasOne(x => x.GovernmentApplication)
            .WithMany()
            .HasForeignKey(x => x.GovernmentApplicationId);
    }
}
