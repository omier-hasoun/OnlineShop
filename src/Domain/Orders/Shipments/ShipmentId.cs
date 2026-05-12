namespace Domain.Orders.Shipments;


public readonly record struct ShipmentId
{
    public long Value { get; }
    public ShipmentId(long value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value <= 0)
        {
            return DomainErrors.Shipments.ShipmentIdInvalid;
        }

        return Result.Success;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

