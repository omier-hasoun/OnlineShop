
namespace Domain.Common.Rules;

public static class OrderItemRules
{
    public const byte MinQuantityValue = 1;
    public const short MaxQuantityValue = 1000;

    public const byte MinUnitPriceValue = ProductVariantRules.MinOriginalPriceValue;
    public const int MaxUnitPriceValue = ProductVariantRules.MaxOriginalPriceValue;

    public const byte MinTotalPriceValue = ProductVariantRules.MinOriginalPriceValue;
    public const int MaxTotalPriceValue = 1_000_000;


}
