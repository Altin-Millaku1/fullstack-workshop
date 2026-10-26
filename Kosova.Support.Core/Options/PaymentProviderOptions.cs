namespace Kosova.Support.Core.Options;

public class PaymentProviderOptions
{
    public const string SectionName = "PaymentProvider";
    public string ProviderName { get; set; } = "Sandbox";
    public string ApiKey { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
}
