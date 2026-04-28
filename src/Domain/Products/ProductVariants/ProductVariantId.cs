
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

    public static ProductVariantId Parse(string value)
    {
        if (TryParse(value, out var id))
            return id;
        throw new ArgumentException("ProductVariantId is invalid.", nameof(value));
    }
    public static bool TryParse(string value, out ProductVariantId id)
    {
        if (long.TryParse(value, out var brandId))
        {
            id = new ProductVariantId(brandId);
            return true;
        }
        id = new();
        return false;
    }
}
