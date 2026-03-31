using Domain.Common.ValidationRules;

namespace Domain.Products.Reviews;

public sealed class ProductReview : BaseEntity
{
    private ProductReview()
    {

    }

    public static Result<ProductReview> Create(ProductReviewId id, ProductId productId, UserId customerId,
        int rating, string comment)
    {
        if (rating < ProductRules.MinRatingValue || rating > ProductRules.MaxRatingValue)
        {
            throw new ArgumentOutOfRangeException(nameof(rating), $"Rating must be between {ProductRules.MinRatingValue} and {ProductRules.MaxRatingValue}.");
        }
        if (comment.Length < ReviewRules.MinReviewCommentLength || comment.Length > ReviewRules.MaxReviewCommentLength)
        {
            throw new ArgumentException($"Comment length must be between {ReviewRules.MinReviewCommentLength} and {ReviewRules.MaxReviewCommentLength} characters.", nameof(rating));
        }


        return new ProductReview()
        {
            Id = id,
            ProductId = productId,
            UserId = customerId,
            Rating = rating,
            Comment = comment
        };
    }

    public ProductReviewId Id { get; private init; }
    public ProductId ProductId { get; private set; }
    public UserId UserId { get; private set; }
    public int Rating
    {
        get;
        private set;
    }
    public string Comment
    {
        get;
        private set;
    } = null!;

    public Product? ProductInfo { get; private set; }
    public User? CustomerInfo { get; private set; }

    public void ModifyReview(int newRating, string newComment)
    {
        Rating = newRating;
        Comment = newComment;
    }
}
