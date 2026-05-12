
namespace Domain.Products;
public readonly record struct ProductId
{
    public long Value { get; }
    public ProductId(long value)
    {
        Value = value;
    }

    public Result<Success> Validate()
    {
        if (Value <= 0)
        {
            return DomainErrors.Products.ProductIdInvalid;
        }

        return Result.Success;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
