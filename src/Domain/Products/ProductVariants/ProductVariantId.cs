
namespace Domain.Products.ProductVariants;

public readonly record struct ProductVariantId
{
    public long Value { get; }
    public ProductVariantId(long value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value <= 0)
        {
            return DomainErrors.ProductVariants.ProductVariantIdInvalid;
        }

        return Result.Success;
    }
}
