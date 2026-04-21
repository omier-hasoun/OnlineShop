
namespace Domain.Products.ProductVariants;

public readonly record struct ProductVariantId
{
    public long Value { get; }

    public ProductVariantId(long value)
    {
        if (value <= 0)
            throw new ArgumentException("ProductVariantId is invalid.", nameof(value));

        Value = value;
    }

    public static implicit operator long(ProductVariantId productVariantId) => productVariantId.Value;
    public static implicit operator ProductVariantId(long value) => new ProductVariantId(value);
}
