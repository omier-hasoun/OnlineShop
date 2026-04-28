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

    public static ProductReviewId Parse(string value)
    {
        if (TryParse(value, out var id))
            return id;
        throw new ArgumentException("ProductReviewId is invalid.", nameof(value));
    }
    public static bool TryParse(string value, out ProductReviewId id)
    {
        if (long.TryParse(value, out var brandId))
        {
            id = new ProductReviewId(brandId);
            return true;
        }
        id = new();
        return false;
    }
}
