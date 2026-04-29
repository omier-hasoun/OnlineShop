
namespace Domain.Customers.CustomerShippingAddresses;

public readonly record struct CustomerShippingAddressId
{
    public long Value { get; }
    public CustomerShippingAddressId(long value)
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
