

namespace Domain.Customers;

public readonly record struct CustomerId
{
    public static readonly CustomerId Empty = new ();

    public Guid Value { get; init; }

    public CustomerId(Guid value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value.Version != 7)
        {
            return DomainErrors.Customers.CustomerIdInvalid;
        }

        return Result.Success;
    }
}
