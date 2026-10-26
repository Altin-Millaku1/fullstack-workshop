using System.Security.Claims;
using Kosova.Support.Core.Entities;
using Kosova.Support.Core.Enums;
using Kosova.Support.Core.Interfaces;
using Kosova.Support.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Controllers;

[Authorize]
public class PaymentsController(KosovaSupportDbContext dbContext, IPaymentProvider paymentProvider) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var payments = await dbContext.Payments.Include(x => x.Application).Where(x => x.Application!.UserId == userId).ToListAsync();
        var pending = await dbContext.Applications.Include(x => x.Service).Where(x => x.UserId == userId && x.Status == ApplicationStatus.Submitted).ToListAsync();
        ViewBag.PendingApplications = pending;
        return View(payments);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Pay(int applicationId)
    {
        var app = await dbContext.Applications.Include(x => x.Service).FirstOrDefaultAsync(x => x.Id == applicationId);
        if (app is null || app.Service is null)
        {
            return NotFound();
        }

        var result = await paymentProvider.ChargeAsync(new PaymentRequest(app.Id, app.Service.FeeAmount, "EUR", User.Identity?.Name ?? string.Empty));

        dbContext.Payments.Add(new Payment
        {
            ApplicationId = app.Id,
            Amount = app.Service.FeeAmount,
            Currency = "EUR",
            ExternalReference = result.ExternalReference,
            Status = result.Success ? PaymentStatus.Completed : PaymentStatus.Failed
        });

        await dbContext.SaveChangesAsync();
        TempData[result.Success ? "Success" : "Error"] = result.Message;
        return RedirectToAction(nameof(Index));
    }
}
