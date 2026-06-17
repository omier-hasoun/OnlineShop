
namespace Domain.Services;

public sealed class ShippingCostCalculator
{
    public static readonly Money FreeShippingThreshold =
        Money.Create(49.99m);

    public static readonly Money StandardShippingFee =
        Money.Create(5.99m);

    public Money Calculate(Money itemsSubtotal)
    {
        return itemsSubtotal >= FreeShippingThreshold
            ? Money.Zero
            : StandardShippingFee;
    }
}
