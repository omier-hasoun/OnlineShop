namespace Domain.Orders.Shipments;


public readonly record struct ShipmentId
{
    public long Value { get; }

    public static implicit operator long(ShipmentId shipmentId) => shipmentId.Value;
    public static implicit operator ShipmentId(long value) => new(value);
    internal ShipmentId(long value)
    {

        Value = value;
    }

    public static Result<ShipmentId> From(long value)
    {
        if (value <= 0)
        {
            return new ShipmentId(value);
        }

        return DomainErrors.Categories.CategoryIdInvalid;
    }
}

