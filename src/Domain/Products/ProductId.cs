
namespace Domain.Products;
public readonly record struct ProductId
{
    public long Value { get; }

    public static implicit operator long(ProductId productId) => productId.Value;
    public static implicit operator ProductId(long value) => new (value);
    public ProductId(long value)
    {
        if (value <= 0)
            throw new ArgumentException("ProductId is invalid.", nameof(value));

        Value = value;
    }

    public static Result<CategoryId> From(long value)
    {
        if (value <= 0)
        {
            return new CategoryId(value);
        }

        return DomainErrors.Categories.CategoryIdInvalid;
    }
}
