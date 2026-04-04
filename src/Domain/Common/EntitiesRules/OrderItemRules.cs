using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.EntitiesRules;

public static class OrderItemRules
{
    public const byte MinQuantityValue = 1;
    public const short MaxQuantityValue = 10000;

    public const byte MinUnitPriceValue = ProductVariantRules.MinOriginalPriceValue;
    public const int MaxUnitPriceValue = ProductVariantRules.MaxOriginalPriceValue;

    public const byte MinTotalPriceValue = ProductVariantRules.MinOriginalPriceValue;
    public const int MaxTotalPriceValue = 50_000_000;


}
