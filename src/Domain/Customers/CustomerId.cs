

namespace Domain.Customers;

public readonly record struct CustomerId
{
    public static readonly CustomerId EmptyInstance = new CustomerId();
    public Guid Value { get; init; }

    public static implicit operator Guid(CustomerId userId) => userId.Value;
    public static implicit operator CustomerId(Guid value) => new CustomerId(value);

    internal CustomerId(Guid value)
    {
        Value = value;
    }
    public static Result<CustomerId> From(Guid value)
    {
        if (value.Version == 7)
        {
            return new CustomerId(value);
        }

        return DomainErrors.Customers.CustomerIdInvalid;
    }
}
