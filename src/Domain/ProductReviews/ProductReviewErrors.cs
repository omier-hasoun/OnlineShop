

namespace Domain.ProductReviews;

public static class ProductReviewErrors
{
    public static Error CommentLengthOutOfRange =>
        Error.Validation("Product.Review.CommentLength.OutOfRange", $"Review comment can't exceed {ProductReviewRules.MaxCommentLength} characters.");
}
