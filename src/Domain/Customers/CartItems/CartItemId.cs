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

    public static CartItemId Parse(string value)
    {
        if (TryParse(value, out var id))
            return id;
        throw new ArgumentException("CartItemId is invalid.", nameof(value));
    }
    public static bool TryParse(string value, out CartItemId id)
    {
        if (long.TryParse(value, out var brandId))
        {
            id = new CartItemId(brandId);
            return true;
        }
        id = new();
        return false;
    }
}
