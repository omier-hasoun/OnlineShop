
using Domain.Common.Exceptions;

namespace Domain.Products.ValueObjects;

public sealed record AverageRating
{
    public AverageRating(float value)
    {
        if(ValidationHelper.IsOutOfRange((decimal)value, ProductRules.MinAverageRatingValue, ProductRules.MaxAverageRatingValue))
        {
            throw new InvalidProductAverageRatingException(value);
        }

        Value = value;
    }

    public float Value { get; }

}
