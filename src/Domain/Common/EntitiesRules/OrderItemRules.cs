
namespace Domain.Common.EntitiesRules;

public static class OrderItemRules
{
    public const byte MinQuantityValue = ProductRules.MinValueOf_MaxQuantityPerCustomer;
    public const short MaxQuantityValue = ProductRules.MaxValueOf_MaxQuantityPerCustomer;

    public const byte MinUnitPriceValue = ProductVariantRules.MinOriginalPriceValue;
    public const int MaxUnitPriceValue = ProductVariantRules.MaxOriginalPriceValue;

    public const byte MinTotalPriceValue = ProductVariantRules.MinOriginalPriceValue;
    public const int MaxTotalPriceValue = 1_000_000;


}
