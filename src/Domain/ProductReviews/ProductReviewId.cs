
namespace Domain.ProductReviews;

public readonly record struct ProductReviewId
{
    public long Value { get; }
    public ProductReviewId(long value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value <= 0)
        {
            return DomainErrors.ProductReviews.ProductReviewIdInvalid;
        }

        return Result.Success;
    }
}
