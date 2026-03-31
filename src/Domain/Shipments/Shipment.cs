using Domain.Addresses;

namespace Domain.Shipments;

public sealed class Shipment : BaseEntity
{
    private Shipment()
    {
    }

    public static Result<Shipment> Create(ShipmentId id, OrderId orderId, AddressId addressId, 
        DateTimeOffset estimatedDelivery, string trackingNumber, string CarrierName, 
        string? notes = null)
    {
        return new Shipment()
        {
            Id = id,
            OrderId = orderId,
            EstimatedDelivery = estimatedDelivery,
            TrackingNumber = trackingNumber,
            CarrierName = CarrierName,
            AddressId = addressId,
            Notes = notes,

        };
    }

    public ShipmentId Id { get; private init; }
    public OrderId OrderId { get; private set; }
    public AddressId AddressId { get; private set; }

    public DateTimeOffset EstimatedDelivery { get; private set; }
    public DateTimeOffset? ActualDelivery { get; private set; }

    public string TrackingNumber { get; private set; } = null!;
    public string CarrierName { get; private set; } = null!;
    public string? Notes { get; private set; }

    public Address? AddressInfo { get; private set; }

}
