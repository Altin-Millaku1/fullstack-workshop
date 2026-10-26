using System.Security.Claims;
using System.Text;
using Kosova.Support.Web.Data;
using Kosova.Support.Web.Interfaces;
using Kosova.Support.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Controllers;

[Authorize]
public class PaymentsController(ApplicationDbContext db, IPaymentProvider paymentProvider) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var payments = await db.Payments.Include(x => x.GovernmentApplication).Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedAt).ToListAsync();
        var apps = await db.Applications.Include(x => x.Service).Where(x => x.UserId == userId && x.Status != ApplicationStatus.Draft).ToListAsync();
        ViewBag.Applications = apps;
        return View(payments);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Pay(int applicationId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var app = await db.Applications.Include(x => x.Service).FirstAsync(x => x.Id == applicationId && x.UserId == userId);
        var amount = app.Service?.Fee ?? 0;

        var result = await paymentProvider.ProcessAsync(new PaymentRequest(amount, "EUR", $"Payment for {app.ReferenceNumber}", userId));

        var payment = new Payment
        {
            UserId = userId,
            GovernmentApplicationId = applicationId,
            Amount = amount,
            ProviderReference = result.ProviderReference,
            Status = result.Success ? PaymentStatus.Completed : PaymentStatus.Failed,
            PaidAt = result.Success ? DateTime.UtcNow : null
        };

        db.Payments.Add(payment);
        await db.SaveChangesAsync();

        TempData[result.Success ? "Success" : "Error"] = result.Message;
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Receipt(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var payment = await db.Payments.Include(x => x.GovernmentApplication).FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (payment is null)
        {
            return NotFound();
        }

        var text = $"Kosova.Support Payment Receipt\nReference: {payment.ProviderReference}\nAmount: {payment.Amount} {payment.Currency}\nStatus: {payment.Status}\nDate: {payment.PaidAt:O}";
        var bytes = Encoding.UTF8.GetBytes(text);
        return File(bytes, "application/pdf", $"receipt-{payment.ProviderReference}.pdf");
    }
}
