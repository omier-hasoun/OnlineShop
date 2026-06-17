
namespace Domain.Orders.Shipments;

public enum ShipmentStatus
{
    Pending = 1,
    Confirmed = 2,
    Canceled = 3,
    Shipped = 4,// when shipped cannot cancel anymore
    Delivered = 5
}
