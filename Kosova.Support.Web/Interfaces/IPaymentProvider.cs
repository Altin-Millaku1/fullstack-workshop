namespace Kosova.Support.Web.Interfaces;

public record PaymentRequest(decimal Amount, string Currency, string Description, string UserId);
public record PaymentResult(bool Success, string ProviderReference, string Message);

public interface IPaymentProvider
{
    Task<PaymentResult> ProcessAsync(PaymentRequest request, CancellationToken cancellationToken = default);
}
