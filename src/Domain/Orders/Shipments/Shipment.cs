
namespace Domain.Orders.Shipments;

public sealed class Shipment : BaseEntity<ShipmentId>, IHasModificationTime, IHasCreationTime
{
    private Shipment(ShipmentId id, OrderId orderId,
        DateTime estimatedDelivery, string carrierName, string addressFrom, string addressTo,
        ShipmentStatus status, string? trackingNumber, string? notes, DateTime createdAt, DateTime lastModifiedAt)
        : base(id)
    {
        OrderId = orderId;
        EstimatedDelivery = estimatedDelivery;
        TrackingNumber = trackingNumber;
        CarrierName = carrierName;
        AddressFrom = addressFrom;
        AddressTo = addressTo;
        Notes = notes;
        CreatedAt = createdAt;
        LastModifiedAt = lastModifiedAt;
        Status = status;

    }

    public static Result<Shipment> Create(ShipmentId id, OrderId orderId, DateTime estimatedDelivery, string carrierName,
        string addressFrom, string addressTo, string? trackingNumber, string? notes)
    {


        return new Shipment(id, orderId, estimatedDelivery, carrierName, addressFrom, addressTo, ShipmentStatus.Pending, trackingNumber, notes, TimeService.UtcNow, TimeService.UtcNow);
    }

    public OrderId OrderId { get; private init; }

    public DateTime EstimatedDelivery { get; private set; }
    public DateTime? ActualDelivery { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }

    public string AddressFrom { get; private set; } = null!;
    public string AddressTo { get; private set; } = null!;
    public string CarrierName { get; private set; } = null!;
    public string? TrackingNumber { get; private set; } = null!;
    public string? Notes { get; private set; }

    public ShipmentStatus Status { get; private set; }

}
