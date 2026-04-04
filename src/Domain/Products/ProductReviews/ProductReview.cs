using Domain.Common.EntitiesRules;

namespace Domain.Products.ProductReviews;

public sealed class ProductReview : BaseEntity, IHasCreationTime, IHasModificationTime
{
    private ProductReview()
    {

    }

    public static Result<ProductReview> Create(ProductReviewId id, ProductId productId, UserId userId,
        byte rating, string comment)
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
            UserId = userId,
            Rating = rating,
            Comment = comment
        };
    }

    public ProductReviewId Id { get; private init; }
    public ProductId ProductId { get; private set; }
    public UserId UserId { get; private set; }
    public byte Rating { get; private set; }

    public string? Comment { get; private set; }

    public DateTime LastModifiedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public void ModifyReview(byte newRating, string? newComment)
    {
        Rating = newRating;
        Comment = newComment;
    }
}
