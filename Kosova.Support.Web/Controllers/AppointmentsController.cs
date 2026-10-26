using System.Security.Claims;
using Kosova.Support.Web.Data;
using Kosova.Support.Web.Models;
using Kosova.Support.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Controllers;

[Authorize]
public class AppointmentsController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var appointments = await db.Appointments.Include(x => x.Service).Where(x => x.UserId == userId).OrderBy(x => x.AppointmentTime).ToListAsync();
        ViewBag.Services = await db.Services.Where(x => x.IsActive).ToListAsync();
        return View(appointments);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Book(BookAppointmentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Invalid appointment details.";
            return RedirectToAction(nameof(Index));
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        db.Appointments.Add(new Appointment
        {
            UserId = userId,
            ServiceId = model.ServiceId,
            AppointmentTime = DateTime.SpecifyKind(model.AppointmentTime, DateTimeKind.Utc),
            Location = model.Location,
            Status = "Booked"
        });
        await db.SaveChangesAsync();
        TempData["Success"] = "Appointment booked.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var appointment = await db.Appointments.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (appointment is not null)
        {
            appointment.Status = "Cancelled";
            await db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
