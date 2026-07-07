
namespace Domain.Common.Rules;

public static class OrderRules
{
    public const int FreeShippingThreshold = 50;

    public const byte MinOrderLinesNumber = 1;
    public const short MaxOrderLinesNumber = 100;

    public const byte MinShippingFeesValue = 0;
    public const byte MaxShippingFeesValue = 100;

    public const byte MinTotalItemsPriceValue = 5;
    public const int MaxTotalItemsPriceValue = 100_000_000;
}
