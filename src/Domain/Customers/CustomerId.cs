
namespace Domain.Customers;

public readonly record struct CustomerId
{
    public Guid Value { get; init; }
    public CustomerId(Guid value)
    {
        if (Guid.Empty == value)
        {
            throw new ArgumentException("CustomerId cannot be an empty GUID.", nameof(value));
        }
        Value = value;
    }
}
