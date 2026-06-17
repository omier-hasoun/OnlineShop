
using Application.Features.Public.Checkout.Dtos;

namespace Application.Common.Abstractions;

public interface ICheckoutProvider
{
    Task<string> BeginCheckout(CheckoutSessionInfo info, CancellationToken ct);
}
