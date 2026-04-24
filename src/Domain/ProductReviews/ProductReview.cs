
namespace Domain.ProductReviews;

public sealed class ProductReview : AggregateRoot<ProductReviewId>, IHasCreationTime, IHasModificationTime
{
    private ProductReview()
    {
        
    }
    private ProductReview(ProductReviewId id, ProductId productId, UserId customerId,
        byte rating, string title, string? comment, DateTime createdAt, DateTime lastModifiedAt)
        : base(id)
    {
        ProductId = productId;
        CustomerId = customerId;
        Rating = rating;
        Title = title;
        Comment = comment;
        CreatedAt = createdAt;
        LastModifiedAt = lastModifiedAt;
    }

    public static Result<ProductReview> Create(ProductReviewId id, ProductId productId, UserId userId,
        byte rating, string title, string? comment)
    {
        //if (rating < ProductRules.MinRatingValue || rating > ProductRules.MaxRatingValue)
        //{
        //    throw new ArgumentOutOfRangeException(nameof(rating), $"Rating must be between {ProductRules.MinRatingValue} and {ProductRules.MaxRatingValue} Stars.");
        //}
        //if (comment.Length < ReviewRules.MinReviewCommentLength || comment.Length > ReviewRules.MaxReviewCommentLength)
        //{
        //    throw new ArgumentException($"Comment length must be between {ReviewRules.MinReviewCommentLength} and {ReviewRules.MaxReviewCommentLength} characters.", nameof(rating));
        //}


        // init defaults
        DateTime createdAt = TimeService.UtcNow;
        DateTime lastModifiedAt = createdAt;


        return new ProductReview(id, productId, userId, rating, title, comment, createdAt, lastModifiedAt);
    }

    public ProductId ProductId { get; private set; }
    public UserId CustomerId { get; private set; }
    public byte Rating { get; private set; }

    public string? Comment { get; private set; }
    public string Title { get; private set; } = null!;

    public DateTime LastModifiedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public void ModifyReview(byte newRating, string? newComment)
    {
        Rating = newRating;
        Comment = newComment;
    }
}
