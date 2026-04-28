
namespace Domain.Customers.CustomerShippingAddresses;

public readonly record struct CustomerShippingAddressId
{    
    public long Value { get; }

    public static implicit operator long(CustomerShippingAddressId addressId) => addressId.Value;
    public static implicit operator CustomerShippingAddressId(long value) => new(value);
    public CustomerShippingAddressId(long value)
    {
        if (value <= 0)
            throw new ArgumentException("CustomerShippingAddressId must be a positive integer.", nameof(value));

        Value = value;
    }

    public static CustomerShippingAddressId Parse(string value)
    {
        if (TryParse(value, out var id))
            return id;
        throw new ArgumentException("CustomerShippingAddressId is invalid.", nameof(value));
    }
    public static bool TryParse(string value, out CustomerShippingAddressId id)
    {
        if (long.TryParse(value, out var brandId))
        {
            id = new CustomerShippingAddressId(brandId);
            return true;
        }
        id = new();
        return false;
    }
}
