

namespace Domain.ProductReviews;

public static class ProductReviewErrors
{
    public static Error CommentLengthOutOfRange =>
        Error.Validation("Product.Review.CommentLength.OutOfRange", $"Review comment must be between {ReviewRules.MinReviewCommentLength} and {ReviewRules.MaxReviewCommentLength} characters long.");
}
