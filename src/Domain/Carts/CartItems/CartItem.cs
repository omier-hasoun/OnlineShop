namespace Domain.Carts.CartItems;

public sealed class CartItem : BaseEntity<CartItemId>, IHasCreationTime
{

    private CartItem(CartItemId id, CartId cartId, ProductId productId, short quantity, DateTime createdAt) : base(id)
    {
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
        CreatedAt = createdAt;
    }

    public static Result<CartItem> Create(CartItemId id, CartId cartId, ProductId productId, short quantity)
    {
        var validationResult = Result.ValidateAll(
                                 () => id.IsValid(),
                                 () => productId.IsValid(),
                                 () => ValidateQuantity(quantity)
                                 );

        if (validationResult.Failed)
        {
            return validationResult.Errors;
        }


        return new CartItem(id, cartId, productId, quantity, DateTime.UtcNow);
    }

    public CartId CartId { get; private init; }
    public ProductId ProductId { get; private init; }
    public short Quantity { get; private set; }
    public DateTime CreatedAt { get; set; }

    public Result<Updated> UpdateQuantity(short newQuantity)
    {
        var validationResult = ValidateQuantity(newQuantity);

        if (validationResult.Failed)
        {
            return validationResult.Errors;
        }

        Quantity = newQuantity;

        return Result.Updated;
    }


    private static Result<Success> ValidateQuantity(short quantity)
    {
        if (ValHelper.IsOutOfRange(quantity, CartItemRules.MinQuantity, CartItemRules.MaxQuantity))
        {
            return DomainErrors.Carts.QuantityOutOfRange;
        }

        return Result.Success;
    }
}
