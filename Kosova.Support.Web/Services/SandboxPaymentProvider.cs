using Kosova.Support.Web.Interfaces;

namespace Kosova.Support.Web.Services;

public class SandboxPaymentProvider(ILogger<SandboxPaymentProvider> logger) : IPaymentProvider
{
    public Task<PaymentResult> ProcessAsync(PaymentRequest request, CancellationToken cancellationToken = default)
    {
        var success = request.Amount >= 0;
        var reference = $"SBX-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N")[..8]}";
        logger.LogInformation("Sandbox payment executed for user {UserId} with reference {Reference}", request.UserId, reference);
        return Task.FromResult(new PaymentResult(success, reference, success ? "Sandbox payment completed" : "Sandbox payment failed"));
    }
}
