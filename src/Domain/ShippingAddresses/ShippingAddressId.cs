namespace Domain.ShippingAddresses;

public readonly record struct ShippingAddressId
{
    public long Value { get; }
    public ShippingAddressId(long value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value <= 0)
        {
            return DomainErrors.ShippingAddresses.CustomerShippingAddressesIdInvalid;
        }

        return Result.Success;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
