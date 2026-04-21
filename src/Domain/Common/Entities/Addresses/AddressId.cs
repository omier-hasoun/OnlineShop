namespace Domain.Common.Entities.Addresses;

public readonly record struct AddressId
{
    public long Value { get; }

    public static implicit operator long(AddressId addressId) => addressId.Value;
    public static implicit operator AddressId(long value) => new(value);
    public AddressId(long value)
    {
        if (value <= 0)
            throw new ArgumentException("AddressId must be a positive integer.", nameof(value));

        Value = value;
    }

}
