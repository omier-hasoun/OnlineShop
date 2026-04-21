
namespace Domain.Orders.Shipments;

public enum ShipmentStatus
{
    Pending = 1,
    Confirmed = 2,
    Shipped,
    InTransit,
    OutForDelivery,
    Delivered,
    Canceled
}
