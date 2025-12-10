
namespace Domain.Customers.Addresses;

public readonly record struct CustomerAddressId
{
    public int Value { get; }

    public CustomerAddressId(int value)
    {
        if (value <= 0)
            throw new ArgumentException("AddressId must be a positive integer.", nameof(value));

        Value = value;
    }

}
