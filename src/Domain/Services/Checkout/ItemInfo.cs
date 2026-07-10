
using Domain.Inventories;

namespace Domain.Services.Checkout;

public sealed record ItemInfo(Product Product, IReadOnlyList<Inventory> inventories, ProductGroup Group, short Quantity)
{
    public OrderLineId Id { get; set; }
}
