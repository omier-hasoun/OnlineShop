
namespace Domain.Services.Checkout;

public static class CheckoutRules
{
    public const byte MaxQuantityForSerializedProductsPerOrder = 3;
    public const short MaxQuantityForNonSerializedProductsPerOrder = 1000;

}
