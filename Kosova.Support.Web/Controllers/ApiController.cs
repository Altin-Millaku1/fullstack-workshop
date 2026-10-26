using Kosova.Support.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiController(ApplicationDbContext db) : ControllerBase
{
    [HttpGet("status")]
    public async Task<IActionResult> Status()
    {
        return Ok(new
        {
            name = "Kosova.Support",
            mode = "Prototype",
            timeUtc = DateTime.UtcNow,
            services = await db.Services.CountAsync(),
            activeAlerts = await db.EmergencyAlerts.CountAsync(x => x.IsActive)
        });
    }
}
