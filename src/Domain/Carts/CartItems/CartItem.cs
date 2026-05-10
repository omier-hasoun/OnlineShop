namespace Domain.Carts.CartItems;

public sealed class CartItem : BaseEntity<CartItemId>, IHasCreationTime
{

    private CartItem(CartItemId id, CartId cartId, ProductVariantId productVariantId, short quantity, DateTime createdAt) : base(id)
    {
        CartId = cartId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
        CreatedAt = createdAt;
    }

    public static Result<CartItem> Create(CartItemId id, CartId cartId, ProductVariantId productVariantId, short quantity)
    {
        var validationResult = Result.ValidateAll(
                                 () => id.IsValid(),
                                 () => productVariantId.IsValid(),
                                 () => ValidateQuantity(quantity)
                                 );

        if (validationResult.Failed)
        {
            return validationResult.Errors;
        }


        return new CartItem(id, cartId, productVariantId, quantity, DateTime.UtcNow);
    }

    public CartId CartId { get; private init; }
    public ProductVariantId ProductVariantId { get; private init; }
    public short Quantity { get; private set; }
    public DateTime CreatedAt { get; set; }

    public Result<Success> UpdateQuantity(short newQuantity)
    {
        var validationResult = ValidateQuantity(newQuantity);

        if (validationResult.Failed)
        {
            return validationResult.Errors;
        }

        Quantity = newQuantity;

        return Result.Success;
    }


    private static Result<Success> ValidateQuantity(short quantity)
    {
        if (quantity < CartItemRules.MinQuantity || quantity > CartItemRules.MaxQuantity)
        {
            return DomainErrors.CartItems.QuantityOutOfRange;
        }

        return Result.Success;
    }
}
