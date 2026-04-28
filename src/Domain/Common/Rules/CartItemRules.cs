

namespace Domain.Common.Rules;

public static class CartItemRules
{
    public const byte MinQuantity = 1;

    public const byte MaxQuantityForSerializedProducts = 5;
    public const short MaxQuantityForNonSerialized = 1000;

}

