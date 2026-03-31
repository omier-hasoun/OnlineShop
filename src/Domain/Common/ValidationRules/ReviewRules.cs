
namespace Domain.Common.ValidationRules;

public static class ReviewRules
{
    public const byte MinReviewCommentLength = 0;
    public const byte MaxReviewCommentLength = 128;

    public const byte MinRatingValue = ProductRules.MinRatingValue;
    public const byte MaxRatingValue = ProductRules.MaxRatingValue;
}
