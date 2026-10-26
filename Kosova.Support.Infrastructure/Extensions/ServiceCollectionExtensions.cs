using Kosova.Support.Core.Interfaces;
using Kosova.Support.Infrastructure.Data;
using Kosova.Support.Infrastructure.Identity;
using Kosova.Support.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kosova.Support.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKosovaSupportInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

        services.AddDbContext<KosovaSupportDbContext>(options =>
            options.UseSqlServer(connectionString));

        services
            .AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<KosovaSupportDbContext>();

        services.AddScoped<IPaymentProvider, SandboxPaymentProvider>();
        services.AddScoped<IFileScanner, PlaceholderFileScanner>();
        services.AddScoped<AuditService>();

        return services;
    }
}
