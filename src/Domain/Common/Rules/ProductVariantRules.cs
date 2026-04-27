
namespace Domain.Common.Rules;

public static class ProductVariantRules
{
    public const byte MinOriginalPriceValue = 5;
    public const int MaxOriginalPriceValue = 500_000;

    public const byte MaxDiscountPercentageValue = 80;// max discount is 80 percent
    public const byte MinDiscountPercentageValue = 0;// 0 when no discount is applied

    public const byte MinDiscountPriceValue = MinOriginalPriceValue;
    public const int MaxDiscountPriceValue = 499_500;

    public const byte MinSkuLength = 8;
    public const byte MaxSkuLength = 50;

    public const byte MinNumberOfImages = 0;
    public const byte MaxNumberOfImages = 12;


}
