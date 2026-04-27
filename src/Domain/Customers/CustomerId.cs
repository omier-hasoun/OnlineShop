

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
            throw new ArgumentException("UserId is invalid.", nameof(value));

        Value = value;
    }

}
