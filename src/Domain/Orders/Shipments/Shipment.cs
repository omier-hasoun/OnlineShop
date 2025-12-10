namespace Domain.Orders.Shipments;

public sealed class Shipment : BaseEntity
{
    private Shipment()
    {
    }

    public static Shipment Create(
        OrderId orderId, DateTimeOffset estimatedDelivery, string trackingNumber, string CarrierName,
        DateTimeOffset? actualDelivery = default)
    {
        return new()
        {
            OrderId = orderId,
            EstimatedDelivery = estimatedDelivery,
            ActualDelivery = actualDelivery,
            TrackingNumber = trackingNumber,
            CarrierName = CarrierName

        };
    }

    public OrderId OrderId { get; private set; }
    public CustomerAddressId AddressId { get; private set; }

    public DateTimeOffset EstimatedDelivery { get; private set; }
    public DateTimeOffset? ActualDelivery { get; private set; }
    public string TrackingNumber { get; private set; } = null!;
    public string CarrierName { get; private set; } = null!;
    public string? Notes { get; private set; }

    public CustomerAddress? AddressInfo { get; private set; }
}
