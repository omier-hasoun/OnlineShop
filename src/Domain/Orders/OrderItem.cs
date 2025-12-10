
namespace Domain.Orders;

public sealed class OrderItem : BaseEntity
{
    private OrderItem()
    {
    }

    public static OrderItem Create(OrderId orderId, ProductId productId, int quantity, decimal unitPrice)
    {
        return new()
        {
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = unitPrice
        };
    }

    public OrderId OrderId { get; private set; }
    public ProductId ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalPrice { get; private set; }
    public short Units { get; private set; }

    public Order? OrderInfo { get; private set; }
    public Product? ProductInfo { get; private set; }
}
