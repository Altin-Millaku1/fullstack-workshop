using Kosova.Support.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await db.Database.MigrateAsync();

        if (!await db.ServiceCategories.AnyAsync())
        {
            var categories = new List<ServiceCategory>
            {
                new() { Name = "Dokumente identiteti", NameEn = "Identity and personal documents", NameSr = "Identitet i lična dokumenta" },
                new() { Name = "Regjistrimi civil", NameEn = "Civil registration", NameSr = "Civilna registracija" },
                new() { Name = "Tatime dhe financa", NameEn = "Tax and finance", NameSr = "Porezi i finansije" },
                new() { Name = "Shëndetësi", NameEn = "Health", NameSr = "Zdravstvo" },
                new() { Name = "Arsim", NameEn = "Education", NameSr = "Obrazovanje" },
                new() { Name = "Punësim", NameEn = "Employment", NameSr = "Zapošljavanje" },
                new() { Name = "Regjistrim biznesi", NameEn = "Business registration", NameSr = "Registracija biznisa" },
                new() { Name = "Shërbime sociale", NameEn = "Social services", NameSr = "Socijalne usluge" },
                new() { Name = "Transport dhe automjete", NameEn = "Transport and vehicle services", NameSr = "Prevoz i vozila" },
                new() { Name = "Pronë dhe komuna", NameEn = "Property and municipal services", NameSr = "Imovina i opštinske usluge" },
                new() { Name = "Imigrim dhe qëndrim", NameEn = "Immigration and residence", NameSr = "Imigracija i boravak" },
                new() { Name = "Shërbime ligjore", NameEn = "Legal and administrative services", NameSr = "Pravne i administrativne usluge" },
                new() { Name = "Siguri publike", NameEn = "Public safety", NameSr = "Javna bezbednost" },
                new() { Name = "Mjedis", NameEn = "Environment", NameSr = "Životna sredina" },
                new() { Name = "Turizëm", NameEn = "Tourism", NameSr = "Turizam" }
            };
            db.ServiceCategories.AddRange(categories);
        }

        if (!await db.Departments.AnyAsync())
        {
            db.Departments.AddRange(
                new Department { Name = "Agjencia për Regjistrim Civil", NameEn = "Civil Registration Agency", NameSr = "Agencija za civilnu registraciju", Email = "civil@example.demo" },
                new Department { Name = "Administrata Tatimore", NameEn = "Tax Administration", NameSr = "Poreska uprava", Email = "tax@example.demo" },
                new Department { Name = "Ministria e Shëndetësisë", NameEn = "Ministry of Health", NameSr = "Ministarstvo zdravlja", Email = "health@example.demo" }
            );
        }

        await db.SaveChangesAsync();

        if (!await db.Services.AnyAsync())
        {
            var category = await db.ServiceCategories.FirstAsync();
            var department = await db.Departments.FirstAsync();
            db.Services.AddRange(
                new Service
                {
                    Title = "Aplikim për pasaportë",
                    TitleEn = "Passport application",
                    TitleSr = "Prijava za pasoš",
                    Description = "Shërbim demonstrues për aplikim online të pasaportës.",
                    DescriptionEn = "Demo service for online passport application.",
                    DescriptionSr = "Demo usluga za online prijavu pasoša.",
                    Fee = 25,
                    EstimatedDays = 5,
                    ServiceCategoryId = category.Id,
                    DepartmentId = department.Id
                },
                new Service
                {
                    Title = "Certifikatë lindjeje",
                    TitleEn = "Birth certificate",
                    TitleSr = "Izvod rođenih",
                    Description = "Kërkesë për certifikatë lindjeje në format digjital.",
                    DescriptionEn = "Request birth certificate in digital format.",
                    DescriptionSr = "Zahtev za izvod rođenih u digitalnom formatu.",
                    Fee = 3,
                    EstimatedDays = 1,
                    ServiceCategoryId = category.Id,
                    DepartmentId = department.Id
                }
            );
        }

        if (!await db.NewsArticles.AnyAsync())
        {
            db.NewsArticles.Add(new NewsArticle
            {
                Title = "Mirë se vini në Kosova.Support",
                TitleEn = "Welcome to Kosova.Support",
                TitleSr = "Dobrodošli na Kosova.Support",
                Content = "Ky është portal demonstrues për shërbime publike.",
                IsAnnouncement = true,
                PublishedAt = DateTime.UtcNow
            });
        }

        if (!await db.Events.AnyAsync())
        {
            db.Events.Add(new Event
            {
                Title = "Dita e Informimit për Shërbimet Digjitale",
                Description = "Ngjarje demonstrative për përdorimin e portalit.",
                EventDate = DateTime.UtcNow.AddDays(10),
                Location = "Prishtinë"
            });
        }

        if (!await db.EmergencyAlerts.AnyAsync())
        {
            db.EmergencyAlerts.Add(new EmergencyAlert
            {
                Title = "Njoftim demonstrues",
                Message = "Kjo është një faqe prototip dhe përdor të dhëna shembull.",
                IsActive = true,
                StartsAt = DateTime.UtcNow.AddDays(-1),
                EndsAt = DateTime.UtcNow.AddDays(7)
            });
        }

        if (!await db.FAQs.AnyAsync())
        {
            db.FAQs.AddRange(
                new FAQ { Question = "Si të krijoj llogari?", Answer = "Përdorni butonin Regjistrohu dhe verifikoni emailin." },
                new FAQ { Question = "A janë pagesat reale?", Answer = "Jo, pagesat janë në sandbox/demonstrim." }
            );
        }

        if (!await db.PublicTenders.AnyAsync())
        {
            db.PublicTenders.Add(new PublicTender
            {
                Title = "Njoftim publik demonstrues",
                Description = "Të dhëna demonstrative për publikim njoftimesh.",
                Deadline = DateTime.UtcNow.AddDays(20)
            });
        }

        if (!await db.SystemSettings.AnyAsync())
        {
            db.SystemSettings.AddRange(
                new SystemSetting { Key = "PortalMode", Value = "Prototype" },
                new SystemSetting { Key = "PaymentProvider", Value = "Sandbox" }
            );
        }

        await db.SaveChangesAsync();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roles = new[] { "Citizen", "Business", "Visitor", "Admin", "ServiceAdmin" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var adminEmail = "admin@kosova.support.demo";
        var admin = await userManager.FindByEmailAsync(adminEmail);

        if (admin is null)
        {
            admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Demo Admin",
                EmailConfirmed = true,
                PreferredLanguage = "sq"
            };
            await userManager.CreateAsync(admin, "Admin123!Demo");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "ServiceAdmin" });
        }
    }
}
