
namespace Domain.Common.Rules;

public static class OrderItemRules
{
    public const byte MinQuantityValue = 1;
    public const short MaxQuantityValue = 1000;

    public const byte MinUnitPriceValue = ProductVariantRules.MinPrice;
    public const int MaxUnitPriceValue = ProductVariantRules.MaxPrice;

    public const byte MinTotalPriceValue = ProductVariantRules.MinPrice;
    public const int MaxTotalPriceValue = 1_000_000;


}
