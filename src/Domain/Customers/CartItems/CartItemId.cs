namespace Domain.Customers.CartItems;

public readonly record struct CartItemId
{
    public long Value { get; init; }

    public static implicit operator long(CartItemId cartId) => cartId.Value;
    public static implicit operator CartItemId(long value) => new CartItemId(value);

    public CartItemId(long value)
    {
        Value = value;
    }

    public static Result<CartItemId> From(long value)
    {
        if (value <= 0)
        {
            return new CartItemId(value);
        }

        return DomainErrors.CartItems.CartItemIdInvalid;
    }
}
