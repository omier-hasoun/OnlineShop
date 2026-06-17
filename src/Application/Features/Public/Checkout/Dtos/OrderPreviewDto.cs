
using Domain.Common.ValueObjects;

namespace Application.Features.Public.Checkout.Dtos;

public sealed record OrderPreviewDto
{
    public OrderPreviewDto(IReadOnlyCollection<OrderItemPreviewDto> items, Money itemsSubtotal, Money shippingCost, Money orderTotal)
    {
        Items = items;
        ItemsSubtotal = itemsSubtotal.Value;
        ShippingCost = shippingCost.Value;
        OrderTotal = orderTotal.Value;
    }

    public IReadOnlyCollection<OrderItemPreviewDto> Items { get; }
    public decimal ItemsSubtotal { get; }
    public decimal ShippingCost { get; }
    public decimal OrderTotal { get; }

}
