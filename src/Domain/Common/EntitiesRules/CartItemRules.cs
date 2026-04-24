

namespace Domain.Common.EntitiesRules;

public static class CartItemRules
{
    public const byte MinQuantityValue = ProductRules.MinValueOf_MaxQuantityPerCustomer;
    public const short MaxQuantityValue = ProductRules.MaxValueOf_MaxQuantityPerCustomer;
}

