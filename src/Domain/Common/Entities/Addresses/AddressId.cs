namespace Domain.Common.Entities.Addresses;

public readonly record struct AddressId
{
    public long Value { get; }

    public static implicit operator long(AddressId addressId) => addressId.Value;
    public static implicit operator AddressId(long value) => new(value);
    public AddressId(long value)
    {
        if (value <= 0)
            throw new ArgumentException("AddressId is Invalid.", nameof(value));

        Value = value;
    }
    public static AddressId Parse(string value)
    {
        if (TryParse(value, out var id))
            return id;
        throw new ArgumentException("AddressId is invalid.", nameof(value));
    }
    public static bool TryParse(string value, out AddressId id)
    {
        if (long.TryParse(value, out var brandId))
        {
            id = new AddressId(brandId);
            return true;
        }
        id = new();
        return false;
    }
}
