
namespace Domain.Common.EntitiesRules;

public static class ProductVariantRules
{
    public const byte MinOriginalPriceValue = 2;
    public const int MaxOriginalPriceValue = 500_000;

    public const byte MaxDiscountPercentageValue = 80;// max disount is 80 percent
    public const byte MinDiscountPercentageValue = 0;// 0 when no discount is applyed but we dont want to make the field nullable

    public const byte MinDiscountPriceValue = MinOriginalPriceValue;
    public const int MaxDiscountPriceValue = 499_500;

    public static readonly int MinSkuLength = 8;
    public static readonly int MaxSkuLength = 50;
}
