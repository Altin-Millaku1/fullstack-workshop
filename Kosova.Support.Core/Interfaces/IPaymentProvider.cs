namespace Kosova.Support.Core.Interfaces;

public record PaymentRequest(int ApplicationId, decimal Amount, string Currency, string CustomerEmail);
public record PaymentResult(bool Success, string ExternalReference, string Message);

public interface IPaymentProvider
{
    Task<PaymentResult> ChargeAsync(PaymentRequest request, CancellationToken cancellationToken = default);
    Task<PaymentResult> RefundAsync(string externalReference, decimal amount, CancellationToken cancellationToken = default);
}
