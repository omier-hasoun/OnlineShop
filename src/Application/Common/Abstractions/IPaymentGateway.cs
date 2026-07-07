
using Application.Features.Public.Checkout.Dtos;

namespace Application.Common.Abstractions;

public interface IPaymentGateway
{
    Task<(string SessionId, string SessionUrl)> StartPaymentProcessAsync(OrderDetailsDto details, CancellationToken ct);

    Task<(string RefundId, RefundState Status)> RefundAsync(string sessionId, CancellationToken ct);

    Task CancelPaymentProcess(string sessionId, CancellationToken ct);

    Task<PaymentDetailsDto> GetPaymentDetailsAsync(string sessionId, CancellationToken ct);
}
