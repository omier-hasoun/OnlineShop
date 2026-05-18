
namespace Domain.Common.Rules;

public static class ProductRules
{
    public const byte MinPrice = 5;
    public const int MaxPrice = 500_000;

    public const byte MinPriceToApplyADiscount = 10;

    public const byte MaxDiscountPercentageValue = 80;// max discount is 80 percent
    public const byte MinDiscountPercentageValue = 1;// 0 when no discount is applied

    public const byte MinSkuLength = 8;
    public const byte MaxSkuLength = 50;

    public const byte MinSlugLength = 5;
    public const byte MaxSlugLength = 80;

    public const byte MinBarcodeLength = 6;
    public const byte MaxBarcodeLength = 14;

    public const byte Min_Height_Width_Length_cm = 2;
    public const short Max_Height_Width_Length_cm = 400;

    public const byte MinNumberOfImages = 0;
    public const byte MaxNumberOfImages = 10;

    public const byte MinNumberOfSpecifications = 1;

    public const byte MaxNumberOfSpecifications = 50;

    public const byte MinSpecificationKeyLength = 1;

    public const byte MaxSpecificationKeyLength = 40;

    public const byte MinSpecificationValueLength = 1;

    public const byte MaxSpecificationValueLength = 50;
}
