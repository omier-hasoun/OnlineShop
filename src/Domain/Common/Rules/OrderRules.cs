
namespace Domain.Common.Rules;

public static class OrderRules
{
    public const int FreeShippingThreshold = 50;

    public const byte MinOrderItemsCount = 1;
    public const short MaxOrderItemsCount = 3000;

    public const byte MinShippingFeesValue = 0;
    public const byte MaxShippingFeesValue = 100;

    public const byte MinTotalItemsPriceValue = 5;
    public const int MaxTotalItemsPriceValue = 100_000_000;
}
