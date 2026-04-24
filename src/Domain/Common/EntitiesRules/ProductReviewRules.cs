
namespace Domain.Common.EntitiesRules;

public static class ProductReviewRules
{
    public const byte MaxCommentLength = 150;

    public const byte MaxTitleLength = 50;


    public const byte MinRatingValue = 1;
    public const byte MaxRatingValue = 5;
}
