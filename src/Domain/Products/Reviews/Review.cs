using Domain.Common.ValidationRules;

namespace Domain.Products.Reviews;

public sealed class Review : BaseEntity
{
    private Review()
    {

    }

    public static Review Create(ProductId productId,
        CustomerId customerId,
        int rating,
        string comment,
        ReviewId id = default)
    {



        return new()
        {
            Id = id,
            ProductId = productId,
            CustomerId = customerId,
            Rating = rating,
            Comment = comment
        };
    }

    public ReviewId Id { get; private init; }
    public ProductId ProductId { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public int Rating
    {
        get;
        private set
        {
            if (value < ProductRules.ReviewRules.RatingMinValue || value > ProductRules.ReviewRules.RatingMaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"Rating must be between {ProductRules.ReviewRules.RatingMinValue} and {ProductRules.ReviewRules.RatingMaxValue}.");
            }
            field = value;
        }
    }
    public string Comment
    {
        get;
        private set
        {
            if (value.Length < ProductRules.ReviewRules.CommentMinLength || value.Length > ProductRules.ReviewRules.CommentMaxLength)
            {
                throw new ArgumentException($"Comment length must be between {ProductRules.ReviewRules.CommentMinLength} and {ProductRules.ReviewRules.CommentMaxLength} characters.", nameof(value));
            }
            field = value;
        }
    } = null!;

    public Product? ProductInfo { get; private set; }
    public Customer? CustomerInfo { get; private set; }

    public void ModifyReview(int newRating, string newComment)
    {
        Rating = newRating;
        Comment = newComment;
    }
}
