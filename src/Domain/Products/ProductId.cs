
namespace Domain.Products;
public readonly record struct ProductId
{
    public long Value { get; }
    public ProductId(long value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value <= 0)
        {
            return DomainErrors.Products.ProductIdInvalid;
        }

        return Result.Success;
    }
}
