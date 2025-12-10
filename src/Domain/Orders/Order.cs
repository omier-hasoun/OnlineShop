
using Domain.Orders.Shipments;

namespace Domain.Orders;

public sealed class Order : BaseEntity
{

    private Order()
    {
    }

    public static Order Create(
        CustomerId customerId,
        DateTimeOffset orderDate,
        decimal totalAmount,
        OrderId id = default)
    {
        return new()
        {
            Id = id == default ? new OrderId(Guid.CreateVersion7()) : id,
            CustomerId = customerId,
            PlacedAt = orderDate,
            TotalAmount = totalAmount,
            Status = OrderStatus.Processing,

        };
    }
    public OrderId Id { get; private init; }
    public CustomerId CustomerId { get; private set; }
    public DateTimeOffset PlacedAt { get; private set; }
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }

    public Customer? CustomerInfo { get; private set; } = null!;
    public ICollection<OrderItem> OrderItems { get; private set; } = [];
    public Shipment? ShipmentInfo {get; private set;}
}
