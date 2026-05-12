
namespace Domain.Carts;

public readonly record struct CartId
{
    public long Value { get; }
    public CartId(long value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value <= 0)
        {
            return DomainErrors.Carts.CartIdInvalid;
        }

        return Result.Success;
    }
    public override string ToString()
    {
        return Value.ToString();
    }
}
