
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.UrlProviders;

internal sealed class ApplicationUrlProvider : IApplicationUrlProvider
{
    private readonly ApplicationUrlsOptions _options;

    public ApplicationUrlProvider(IOptions<ApplicationUrlsOptions> options)
    {
        _options = options.Value;
    }

    public string BaseUrl => _options.BaseUrl;

    public string ApiUrl => _options.ApiUrl;


    public string GetPaymentSuccessUrl(string paymentId)
    {
        return $"{_options.BaseUrl}/payment/success/{paymentId}";
    }


    public string GetPaymentFailedUrl(string paymentId)
    {
        return $"{_options.BaseUrl}/payment/failed/{paymentId}";
    }
}
