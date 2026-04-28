using Domain.ProductsStock;

namespace Domain.ProductReviews;

public readonly record struct ProductReviewId
{
    public long Value { get; }

    public ProductReviewId(long value)
    {
        if (value <= 0)
            throw new ArgumentException("ProductReviewId is invalid.", nameof(value));

        Value = value;
    }

    public static implicit operator long(ProductReviewId productReviewId) => productReviewId.Value;
    public static implicit operator ProductReviewId(long value) => new (value);

    public static Result<ProductReviewId> From(long value)
    {
        if (value <= 0)
        {
            return new ProductReviewId(value);
        }

        return DomainErrors.ProductReviews.ProductReviewIdInvalid;
    }
}
