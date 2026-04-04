using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.EntitiesRules;

public static class ProductVariantRules
{
    public const byte MinOriginalPriceValue = 3;
    public const int MaxOriginalPriceValue = 10_000_000;

}
