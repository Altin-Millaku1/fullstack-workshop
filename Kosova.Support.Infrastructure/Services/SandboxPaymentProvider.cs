using Kosova.Support.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Kosova.Support.Infrastructure.Services;

public class SandboxPaymentProvider(ILogger<SandboxPaymentProvider> logger) : IPaymentProvider
{
    public Task<PaymentResult> ChargeAsync(PaymentRequest request, CancellationToken cancellationToken = default)
    {
        var reference = $"SBOX-{DateTime.UtcNow:yyyyMMddHHmmss}-{request.ApplicationId}";
        logger.LogInformation("Sandbox payment executed for application {ApplicationId} amount {Amount}", request.ApplicationId, request.Amount);
        return Task.FromResult(new PaymentResult(true, reference, "Sandbox payment authorized."));
    }

    public Task<PaymentResult> RefundAsync(string externalReference, decimal amount, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Sandbox refund executed for reference {Reference} amount {Amount}", externalReference, amount);
        return Task.FromResult(new PaymentResult(true, externalReference, "Sandbox refund successful."));
    }
}
