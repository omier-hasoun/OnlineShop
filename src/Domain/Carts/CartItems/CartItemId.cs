namespace Domain.Carts.CartItems;

public readonly record struct CartItemId
{
    public long Value { get; }
    public CartItemId(long value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value <= 0)
        {
            return DomainErrors.Carts.CartItemIdInvalid;
        }

        return Result.Success;
    }
    public override string ToString()
    {
        return Value.ToString();
    }
}
