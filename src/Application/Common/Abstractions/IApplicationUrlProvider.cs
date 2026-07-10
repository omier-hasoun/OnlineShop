
namespace Application.Common.Abstractions;


public interface IApplicationUrlProvider
{
    string BaseUrl { get; }

    string ApiUrl { get; }

    string GetPaymentSuccessUrl(string paymentId);

    string GetPaymentFailedUrl(string paymentId);
}
