
using Domain.Inventories;

namespace Domain.Services.Checkout;

public sealed record class OrderLineEntities(OrderLineId Id, Product Product, IReadOnlyList<Inventory> inventories, ProductGroup Group, short Quantity)
{
}
