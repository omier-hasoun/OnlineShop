namespace Domain.UserShippingAddresses;

public readonly record struct UserShippingAddressId
{
    public long Value { get; }
    public UserShippingAddressId(long value)
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
