

using Domain.Addresses;
using Domain.Orders.Items;
using Domain.Shipments;

namespace Domain.Orders;

public sealed class Order : BaseEntity
{

    private Order()
    {
    }

    public static Result<Order> Create(OrderId id, UserId customerId, AddressId addressId, decimal totalAmount)
    {
        return new Order()
        {
            Id = id,
            UserId = customerId,
            AddressId = addressId,
            TotalAmount = totalAmount,
            PlacedAt = TimeService.UtcNow,
            Status = OrderStatus.Processing,

        };
    }
    public OrderId Id { get; private init; }
    public UserId UserId { get; private set; }
    public AddressId AddressId { get; private set; }


    public DateTimeOffset PlacedAt { get; private set; }
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }

    public OrderCancelledBy? CancelledBy { get; private set; }
    public string? CancellationReason { get; private set; }
    public DateTimeOffset? CancelledAt { get; private set; }

    public User? CustomerInfo { get; private set; } = null!;
    public ICollection<OrderItem> OrderItems { get; private set; } = [];
    public Shipment? ShipmentInfo {get; private set;}
}
