namespace Domain.Customers.CartItems;

public sealed class CartItem : BaseEntity<CartItemId>, IHasCreationTime
{

    private CartItem(CartItemId id, UserId customerId, ProductVariantId productVariantId, short quantity, DateTime createdAt) : base(id)
    {
        CustomerId = customerId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
        CreatedAt = createdAt;
    }

    public static Result<CartItem> Create(CartItemId id, UserId customerId, ProductVariantId productVariantId, short quantity)
    {

        if (quantity < CartItemRules.MinQuantityValue || quantity > CartItemRules.MaxQuantityValue)
        {
            return CartItemErrors.QuantityOutOfRange;
        }

        return new CartItem(id, customerId, productVariantId, quantity, DateTime.UtcNow);
    }
    public ProductVariantId ProductVariantId { get; private init; }
    public UserId CustomerId { get; private init; }
    public short Quantity { get; private set; }

    public DateTime CreatedAt { get; set; }
}
