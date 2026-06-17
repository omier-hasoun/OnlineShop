using Domain.Services.Models;

namespace Domain.Services.Checkout;

public sealed record OrderPreview(Money Total, Money SubTotal, Money ShippingCost, List<OrderLinePreview> OrderLines)
{
}
