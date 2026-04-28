
namespace Domain.Customers.CustomerShippingAddresses;

public readonly record struct CustomerShippingAddressId
{    
    public long Value { get; }

    public static implicit operator long(CustomerShippingAddressId addressId) => addressId.Value;
    public static implicit operator CustomerShippingAddressId(long value) => new(value);
    public CustomerShippingAddressId(long value)
    {
        if (value <= 0)
            throw new ArgumentException("CustomerShippingAddressId is invalid.", nameof(value));

        Value = value;
    }

    public static Result<CustomerShippingAddressId> From(long value)
    {
        if (value <= 0)
        {
            return new CustomerShippingAddressId(value);
        }

        return DomainErrors.CustomerShippingAddresses.CustomerShippingAddressesIdInvalid;
    }
}
