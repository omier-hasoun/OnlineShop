namespace Domain.Customers.CartItems;

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
            return DomainErrors.Categories.CategoryIdInvalid;
        }

        return Result.Success;
    }
}
