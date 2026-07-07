
namespace Application.Features.Public.Checkout.Dtos;

public sealed record OrderDetailsDto(
string OrderId,
string Currency,
string SuccessUrl,
string CancelUrl,
long ShippingCost,
IReadOnlyCollection<OrderLineDetailsDto> OrderLines
)
{


}
