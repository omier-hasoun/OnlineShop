namespace Domain.CartItems;

public class CartItemErrors
{
    public static Error QuantityRequired =>
        Error.Forbidden("CartItem.Quantity.Required", "The quantity number is required.");
    public static Error QuantityOutOfRange =>
        Error.Forbidden("CartItem.Quantity.OutOfRange", $"The given quantity number must be between {CartItemRules.MinQuantityPerItem} and {CartItemRules.MaxQuantityPerItem} per cart item.");
}
