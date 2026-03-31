namespace Domain.CartItems;

public sealed class CartItem : BaseEntity
{

    private CartItem()
    {

    }

    internal static Result<CartItem> Create(CartItemId id, CartId cartId, ProductId productId, short quantity = 1)
    {
        if (quantity < CartItemRules.MinQuantityPerItem || quantity > CartItemRules.MaxQuantityPerItem)
        {
            return CartItemErrors.QuantityOutOfRange;
        }

        return new CartItem
        {
            Id = id,
            CartId = cartId,
            ProductId = productId,
            Quantity = quantity,
        };
    }
    public CartItemId Id { get; private set; }
    public ProductId ProductId { get; private init; }
    public CartId CartId { get; private set; }

    public short Quantity { get; private set; }
}
