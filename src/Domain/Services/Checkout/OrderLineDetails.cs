
namespace Domain.Services.Checkout;

public sealed record class OrderLineDetails(OrderLineId Id, Product Product, ProductGroup Group, short Quantity)
{
}
