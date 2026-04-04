

namespace Domain.Products.ProductVariants;

public static class ProductVariantErrors
{
    public static Error PriceOutOfRange =>
    Error.Validation("Product.Price.OutOfRange", $"Product price must be at least {ProductVariantRules.MinOriginalPriceValue} and at max {ProductVariantRules.MaxOriginalPriceValue}.");
}
