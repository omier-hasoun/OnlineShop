
namespace Domain.Common.ValueObjects;

public readonly record struct GuestAccountId
{
    public Guid Value { get; init; }

    public GuestAccountId(Guid value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value.Version != 7)// this will also ensure that a guid is not default
        {
            return DomainErrors.GuestIdInvalid;
        }

        return Result.Success;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

