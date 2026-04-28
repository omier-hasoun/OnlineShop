
namespace Domain.Products.ProductVariants;

public readonly record struct ProductVariantId
{
    public long Value { get; }

    internal ProductVariantId(long value)
    {

        Value = value;
    }

    public static implicit operator long(ProductVariantId productVariantId) => productVariantId.Value;
    public static implicit operator ProductVariantId(long value) => new ProductVariantId(value);

    public static Result<ProductVariantId> From(long value)
    {
        if (value <= 0)
        {
            return new ProductVariantId(value);
        }

        return DomainErrors.ProductVariants.ProductVariantIdInvalid;
    }
}
