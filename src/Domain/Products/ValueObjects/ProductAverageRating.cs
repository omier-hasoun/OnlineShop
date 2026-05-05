

namespace Domain.Products.ValueObjects;

public sealed record ProductAverageRating
{
    private ProductAverageRating()
    {
        
    }

    public float Value { get; internal init; } = 0;

    public static Result<ProductAverageRating> From(float value)
    {
        if (ValHelper.IsOutOfRange(value, ProductRules.MinAverageRatingValue, ProductRules.MaxAverageRatingValue))
        {
            return DomainErrors.Products.AverageRatingInvalid;
        }

        return new ProductAverageRating()
        {
            Value = value
        };
    }
}
