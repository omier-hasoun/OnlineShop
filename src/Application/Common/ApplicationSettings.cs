
namespace Application.Common;

public sealed class ApplicationSettings
{
    public ApplicationSettings(string? baseUrl, string? businessName, string orderPaymentSucceededUrl, string orderPaymentFailedUrl)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(baseUrl);
        ArgumentNullException.ThrowIfNullOrEmpty(businessName);
        ArgumentNullException.ThrowIfNullOrEmpty(orderPaymentSucceededUrl);
        ArgumentNullException.ThrowIfNullOrEmpty(orderPaymentFailedUrl);

        BaseUrl = baseUrl;
        BusinessName = businessName;
        OrderPaymentSucceededUrl = orderPaymentSucceededUrl;
        OrderPaymentFailedUrl = orderPaymentFailedUrl;
    }

    public string BaseUrl { get; }
    public string BusinessName { get; }
    public string OrderPaymentSucceededUrl { get; }
    public string OrderPaymentFailedUrl { get; }

}
