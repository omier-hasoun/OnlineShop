namespace Domain.Customers.CartItems;

public readonly record struct CartItemId
{
    public long Value { get; init; }

    public static implicit operator long(CartItemId cartId) => cartId.Value;
    public static implicit operator CartItemId(long value) => new CartItemId(value);

    public CartItemId(long value)
    {
        if (value <= 0)
            throw new ArgumentException("CartItemId cannot be 0.", nameof(value));

        Value = value;
    }

}
