namespace Domain.Customers.CartItems;

public sealed class CartItem : BaseEntity<CartItemId>, IHasCreationTime
{

    private CartItem(CartItemId id, UserId userId, ProductVariantId productVariantId, short quantity, DateTime createdAt) : base(id)
    {
        UserId = userId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
        CreatedAt = createdAt;
    }

    public static Result<CartItem> Create(CartItemId id, UserId userId, ProductVariantId productVariantId, short quantity)
    {

        if (quantity < CartItemRules.MinQuantityValue || quantity > CartItemRules.MaxQuantityValue)
        {
            return CartItemErrors.QuantityOutOfRange;
        }

        return new CartItem(id, userId, productVariantId, quantity, TimeService.UtcNow);
    }
    public ProductVariantId ProductVariantId { get; private init; }
    public UserId UserId { get; private init; }
    public short Quantity { get; private set; }

    public DateTime CreatedAt { get; set; }
}
