namespace Domain.CustomerShippingAddresses;

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
            return DomainErrors.CustomerShippingAddresses.CustomerShippingAddressesIdInvalid;
        }

        return Result.Success;
    }
}
