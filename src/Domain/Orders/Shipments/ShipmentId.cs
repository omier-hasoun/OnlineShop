namespace Domain.Orders.Shipments;


public readonly record struct ShipmentId
{
    public long Value { get; }

    public static implicit operator long(ShipmentId shipmentId) => shipmentId.Value;
    public static implicit operator ShipmentId(long value) => new(value);
    public ShipmentId(long value)
    {
        if (value <= 0)
            throw new ArgumentException("CategoryId is invalid.", nameof(value));

        Value = value;
    }
}

