using System.Security.Claims;
using System.Text;
using Kosova.Support.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kosova.Support.Web.Controllers;

[Authorize]
public class ReceiptsController(KosovaSupportDbContext dbContext) : Controller
{
    public async Task<IActionResult> Application(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var app = await dbContext.Applications.Include(x => x.Service).FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (app is null)
        {
            return NotFound();
        }

        var content = $"Application receipt #{app.Id}\\nService: {app.Service?.Name}\\nStatus: {app.Status}\\nIssued: {DateTime.UtcNow:u}";
        return File(CreateBasicPdf(content), "application/pdf", $"application-receipt-{app.Id}.pdf");
    }

    public async Task<IActionResult> Payment(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var payment = await dbContext.Payments.Include(x => x.Application).FirstOrDefaultAsync(x => x.Id == id && x.Application!.UserId == userId);
        if (payment is null)
        {
            return NotFound();
        }

        var content = $"Payment receipt #{payment.Id}\\nReference: {payment.ExternalReference}\\nAmount: {payment.Amount} {payment.Currency}\\nStatus: {payment.Status}\\nIssued: {DateTime.UtcNow:u}";
        return File(CreateBasicPdf(content), "application/pdf", $"payment-receipt-{payment.Id}.pdf");
    }

    private static byte[] CreateBasicPdf(string text)
    {
        var safe = text.Replace("(", "[").Replace(")", "]");
        var content = $"BT /F1 12 Tf 50 750 Td ({safe.Replace("\\n", ") Tj T* (")}) Tj ET";
        var objects = new[]
        {
            "1 0 obj << /Type /Catalog /Pages 2 0 R >> endobj",
            "2 0 obj << /Type /Pages /Kids [3 0 R] /Count 1 >> endobj",
            "3 0 obj << /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] /Resources << /Font << /F1 4 0 R >> >> /Contents 5 0 R >> endobj",
            "4 0 obj << /Type /Font /Subtype /Type1 /BaseFont /Helvetica >> endobj",
            $"5 0 obj << /Length {content.Length} >> stream\\n{content}\\nendstream endobj"
        };

        var sb = new StringBuilder("%PDF-1.4\\n");
        var offsets = new List<int>();
        foreach (var obj in objects)
        {
            offsets.Add(sb.Length);
            sb.Append(obj).Append('\\n');
        }

        var xrefPosition = sb.Length;
        sb.Append("xref\\n0 6\\n0000000000 65535 f \\n");
        foreach (var off in offsets)
        {
            sb.Append(off.ToString("D10")).Append(" 00000 n \\n");
        }

        sb.Append("trailer << /Size 6 /Root 1 0 R >>\\nstartxref\\n").Append(xrefPosition).Append("\\n%%EOF");
        return Encoding.ASCII.GetBytes(sb.ToString());
    }
}
