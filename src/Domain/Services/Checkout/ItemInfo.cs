
using Domain.Inventories;

namespace Domain.Services.Checkout;

public sealed record ItemInfo(Product Product, Inventory Inventory, ProductGroup Group, short Quantity)
{
    public OrderLineId Id { get; set; }
}
