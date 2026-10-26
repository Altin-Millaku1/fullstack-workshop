using Kosova.Support.Web.Data;
using Kosova.Support.Web.Interfaces;
using Kosova.Support.Web.Models;
using Kosova.Support.Web.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace Kosova.Support.Web.Tests;

public class WorkflowAndPaymentTests
{
    private static ApplicationDbContext CreateDb()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task SandboxPayment_ShouldReturnSuccess_ForNonNegativeAmount()
    {
        IPaymentProvider provider = new SandboxPaymentProvider(new NullLogger<SandboxPaymentProvider>());
        var result = await provider.ProcessAsync(new PaymentRequest(10, "EUR", "Demo", "user1"));

        Assert.True(result.Success);
        Assert.StartsWith("SBX-", result.ProviderReference);
    }

    [Fact]
    public async Task Workflow_Submit_ShouldUpdateStatus()
    {
        await using var db = CreateDb();
        db.Services.Add(new Service { Id = 1, Title = "Test", TitleEn = "Test", TitleSr = "Test", Description = "d", DescriptionEn = "d", DescriptionSr = "d", DepartmentId = 1, ServiceCategoryId = 1 });
        db.Departments.Add(new Department { Id = 1, Name = "Dept", NameEn = "Dept", NameSr = "Dept", Email = "d@example.demo" });
        db.ServiceCategories.Add(new ServiceCategory { Id = 1, Name = "Cat", NameEn = "Cat", NameSr = "Cat" });
        await db.SaveChangesAsync();

        var notifications = new NotificationService(db);
        var audit = new AuditService(db);
        var workflow = new ApplicationWorkflowService(db, notifications, audit);

        var app = await workflow.SaveDraftAsync("user1", 1, "{}");
        await workflow.SubmitAsync(app.Id, "user1");

        var stored = await db.Applications.FindAsync(app.Id);
        Assert.NotNull(stored);
        Assert.Equal(ApplicationStatus.Submitted, stored!.Status);
    }
}
