

namespace Domain.Customers;

public readonly record struct CustomerId
{
    public static readonly CustomerId EmptyInstance = new CustomerId();
    public Guid Value { get; init; }

    public static implicit operator Guid(CustomerId userId) => userId.Value;
    public static implicit operator CustomerId(Guid value) => new CustomerId(value);

    public CustomerId(Guid value)
    {
        if (value.Version != 7 || value == default)
            throw new ArgumentException("CustomerId is invalid.", nameof(value));

        Value = value;
    }
    public static CustomerId Parse(string value)
    {
        if (TryParse(value, out var id))
            return id;
        throw new ArgumentException("CustomerId is invalid.", nameof(value));
    }
    public static bool TryParse(string value, out CustomerId id)
    {
        if (Guid.TryParse(value, out var brandId))
        {
            id = new CustomerId(brandId);
            return true;
        }
        id = new();
        return false;
    }
}
