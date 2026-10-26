using System.Security.Claims;
using Kosova.Support.Core.Entities;
using Kosova.Support.Core.Enums;
using Kosova.Support.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Controllers;

[Authorize]
public class AppointmentsController(KosovaSupportDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var appointments = await dbContext.Appointments.Include(x => x.Service).Where(x => x.UserId == userId).OrderBy(x => x.ScheduledAtUtc).ToListAsync();
        ViewBag.Services = await dbContext.Services.OrderBy(x => x.Name).ToListAsync();
        return View(appointments);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Book(int serviceId, DateTime scheduledAtUtc)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        dbContext.Appointments.Add(new Appointment
        {
            UserId = userId,
            ServiceId = serviceId,
            ScheduledAtUtc = DateTime.SpecifyKind(scheduledAtUtc, DateTimeKind.Utc),
            Status = AppointmentStatus.Scheduled
        });

        await dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var appointment = await dbContext.Appointments.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (appointment is not null)
        {
            appointment.Status = AppointmentStatus.Cancelled;
            await dbContext.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
