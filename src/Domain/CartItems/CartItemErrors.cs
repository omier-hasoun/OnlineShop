namespace Domain.CartItems;

public class CartItemErrors
{
    public static Error ProductVariantInfoIsNull =>
        Error.Validation("CartItem.ProductVariantInfo.Null", "The given product variant information must not be null.");

    public static Error QuantityOutOfRange =>
        Error.Forbidden("CartItem.Quantity.OutOfRange", $"The given quantity number must be between {CartItemRules.MinQuantityPerItem} and {CartItemRules.MaxQuantityPerItem} per cart item.");
}
