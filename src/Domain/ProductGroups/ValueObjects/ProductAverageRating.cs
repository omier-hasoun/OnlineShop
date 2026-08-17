

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Domain.ProductGroups.ValueObjects;

public sealed record ProductAverageRating
{
    [JsonConstructor]// for serialization
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("For serializer only.")]
    public ProductAverageRating(decimal averageRating)
    {
            this.Value = averageRating;

    }

    private ProductAverageRating()
    {
        
    }

    public decimal Value { get; internal init; } = 0;

    public float GetRoundedValue()
    {
        return (float)Math.Round(Value, 2);
    }

    public static Result<ProductAverageRating> From(decimal value)
    {
        if (ValHelper.IsOutOfRange(value, ProductGroupRules.MinAverageRatingValue, ProductGroupRules.MaxAverageRatingValue))
        {
            return DomainErrors.Products.AverageRatingInvalid;
        }

        return new ProductAverageRating()
        {
            Value = value
        };
    }

    public static explicit operator float(ProductAverageRating value) => (float)value.Value;
}
