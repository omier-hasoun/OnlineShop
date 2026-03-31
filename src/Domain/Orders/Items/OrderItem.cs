namespace Domain.Orders.Items;

public sealed class OrderItem : BaseEntity
{
    private OrderItem()
    {
    }

    public static Result<OrderItem> Create(OrderItemId id, OrderId orderId, ProductId productId, short quantity, decimal unitPrice, decimal totalPrice)
    {
        return new OrderItem()
        {
            Id = id,
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = unitPrice,
            TotalPrice = totalPrice,
        };
    }
    public OrderItemId Id { get; private init; }
    public OrderId OrderId { get; private set; }
    public ProductId ProductId { get; private set; }
    public short Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalPrice { get; private set; }

    public Order? OrderInfo { get; private set; }
    public Product? ProductInfo { get; private set; }

}
