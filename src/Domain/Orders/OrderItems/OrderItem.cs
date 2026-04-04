
using Domain.Products.ProductVariants;

namespace Domain.Orders.OrderItems;

public sealed class OrderItem : BaseEntity
{
    private OrderItem()
    {
    }
    public static Result<OrderItem> Create(OrderItemId id, OrderId orderId, CartItem cartItem)
    {


        var productInfo = cartItem.ProductVariantInfo.ProductInfo;
        var productVariantInfo = cartItem.ProductVariantInfo;

        return new OrderItem()
        {

                Id = id,
                OrderId = orderId,
                ProductVariantId = cartItem.ProductVariantId,
                ProductVariantInfo = cartItem.ProductVariantInfo,
                UnitPrice = cartItem.ProductVariantInfo.CurrentPrice,
                Quantity = cartItem.Quantity,
                TotalPrice = cartItem.ProductVariantInfo.CurrentPrice * cartItem.Quantity,
                Status = OrderItemStatus.Pending,
        };

    }

    public OrderItemId Id { get; private init; }
    public OrderId OrderId { get; private init; }
    public ProductVariantId ProductVariantId { get; private init; }

    public short Quantity { get; private init; }

    public decimal UnitPrice { get; private init; }

    public decimal TotalPrice { get; private init; }

    public OrderItemStatus Status { get; private set; }

    public Order? OrderInfo { get; private set; }
    public ProductVariant? ProductVariantInfo { get; private set; }

}
