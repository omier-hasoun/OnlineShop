
namespace Domain.Common.Rules;

public static class OrderItemRules
{
    public const byte MinQuantityValue = 1;
    public const short MaxQuantityValue = 5000;

    public const byte MinUnitPriceValue = ProductRules.MinPrice;
    public const int MaxUnitPriceValue = ProductRules.MaxPrice;

    public const byte MinTotalPriceValue = ProductRules.MinPrice;
    public const int MaxTotalPriceValue = 1_000_000;


}
