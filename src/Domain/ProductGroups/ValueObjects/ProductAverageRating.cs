

namespace Domain.ProductGroups.ValueObjects;

public sealed record ProductAverageRating
{
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
}
