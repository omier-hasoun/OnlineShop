
using Domain.Products.ProductVariants;

namespace Domain.CartItems;

public sealed class CartItem : BaseEntity
{

    private CartItem(CartItemId id, UserId userId, ProductVariant productVariantInfo, short quantity)
    {
        Id = id;
        UserId = userId;
        ProductVariantInfo = productVariantInfo;
        Quantity = quantity;
    }

    public static Result<CartItem> Create(CartItemId id, UserId userId, ProductVariant productVariantInfo, short quantity)
    {
        if(productVariantInfo is null)
        {
            return CartItemErrors.ProductVariantInfoIsNull;
        }
        if (quantity < CartItemRules.MinQuantityPerItem || quantity > CartItemRules.MaxQuantityPerItem)
        {
            return CartItemErrors.QuantityOutOfRange;
        }

        var productInfo = productVariantInfo.ProductInfo;

        return new CartItem(id, userId, productVariantInfo, quantity);
    }
    public CartItemId Id { get; private init; }
    public ProductVariantId ProductVariantId { get; private init; }
    public UserId UserId { get; private init; }
    public short Quantity { get; private set; }

    public ProductVariant ProductVariantInfo { get; private set; } = null!;
}
