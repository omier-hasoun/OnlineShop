

namespace Domain.Products.ValueObjects;

public sealed record AverageRating
{
    internal AverageRating()
    {
        
    }

    public float Value { get; internal init; } = 0;

    public static Result<AverageRating> From(float value)
    {
        if (ValHelper.IsOutOfRange(value, ProductRules.MinAverageRatingValue, ProductRules.MaxAverageRatingValue))
        {
            return DomainErrors.Products.AverageRatingInvalid;
        }

        return new AverageRating()
        {
            Value = value
        };
    }
}
