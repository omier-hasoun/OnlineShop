
namespace Application.Features.Public.Checkout.Dtos;

public sealed record CheckoutSessionInfo(
string ReferenceId, string CustomerEmail, string CurrencyCode,
IEnumerable<OrderItemPreviewDto> OrderItemsDetails
);
