using Kosova.Support.Core.Entities;
using Kosova.Support.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Infrastructure.Data;

public static class SeedData
{
    public static async Task InitializeAsync(KosovaSupportDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await context.Database.MigrateAsync();

        foreach (var role in new[] { "Citizen", "Admin", "ServiceOfficer" })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        const string adminEmail = "admin@kosova.support";
        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin is null)
        {
            admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                FullName = "Platform Administrator",
                IsGovernmentEmployee = true
            };

            await userManager.CreateAsync(admin, "Admin!23456");
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        if (!context.ServiceCategories.Any())
        {
            var categoryNames = new[]
            {
                "Identity and personal documents", "Civil registration", "Tax and finance", "Health", "Education",
                "Employment", "Business registration", "Social services", "Transport and vehicle services",
                "Property and municipal services", "Immigration and residence", "Legal and administrative services",
                "Public safety", "Environment", "Tourism"
            };

            var categories = categoryNames.Select(name => new ServiceCategory
            {
                Name = name,
                Slug = name.ToLowerInvariant().Replace(' ', '-').Replace("and", "").Replace("--", "-")
            }).ToList();

            context.ServiceCategories.AddRange(categories);

            var departments = new[]
            {
                new Department { Name = "Ministry of Internal Affairs", Description = "Identity, registration and residence services." },
                new Department { Name = "Tax Administration", Description = "Tax and business compliance services." },
                new Department { Name = "Municipal Services Office", Description = "Property and local administrative services." }
            };
            context.Departments.AddRange(departments);
            await context.SaveChangesAsync();

            context.Services.AddRange(
                new Service
                {
                    Name = "Passport Renewal",
                    Slug = "passport-renewal",
                    Summary = "Renew your biometric passport using online appointment and document submission.",
                    Eligibility = "Kosovo citizens with expiring or expired passports.",
                    RequiredDocuments = "Existing passport, ID card, recent photo.",
                    FeeAmount = 30,
                    ServiceCategoryId = categories[0].Id,
                    DepartmentId = departments[0].Id
                },
                new Service
                {
                    Name = "Business Registration Certificate",
                    Slug = "business-registration-certificate",
                    Summary = "Apply for a digital business registration certificate.",
                    Eligibility = "Registered or newly registering entities.",
                    RequiredDocuments = "Owner ID, business address proof.",
                    FeeAmount = 20,
                    ServiceCategoryId = categories[6].Id,
                    DepartmentId = departments[1].Id
                },
                new Service
                {
                    Name = "Municipal Property Extract",
                    Slug = "municipal-property-extract",
                    Summary = "Request a municipal property ownership extract.",
                    Eligibility = "Property owners and authorized representatives.",
                    RequiredDocuments = "Owner ID, cadastral reference.",
                    FeeAmount = 8,
                    ServiceCategoryId = categories[9].Id,
                    DepartmentId = departments[2].Id
                }
            );
        }

        if (!context.NewsArticles.Any())
        {
            context.NewsArticles.Add(new NewsArticle
            {
                Title = "Demo platform launch",
                Summary = "Kosova.Support prototype is now available for demonstration.",
                Content = "This website is a prototype and demonstration platform with sandbox integrations only.",
                IsPublished = true,
                PublishedAtUtc = DateTime.UtcNow
            });
        }

        if (!context.EmergencyAlerts.Any())
        {
            context.EmergencyAlerts.Add(new EmergencyAlert
            {
                Title = "Demo Alert",
                Message = "This is a prototype emergency alert used for demonstration purposes only.",
                IsActive = true,
                Severity = "Warning"
            });
        }

        if (!context.FAQs.Any())
        {
            context.FAQs.AddRange(
                new FAQ { Question = "Is this an official government website?", Answer = "No. This is a prototype/demonstration website.", SortOrder = 1 },
                new FAQ { Question = "Can I make real payments?", Answer = "No. Payment integration is sandbox only.", SortOrder = 2 }
            );
        }

        await context.SaveChangesAsync();
    }
}
