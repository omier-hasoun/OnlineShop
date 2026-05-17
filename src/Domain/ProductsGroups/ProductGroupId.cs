
namespace Domain.ProductsGroups;
public readonly record struct ProductGroupId
{
    public long Value { get; }
    public ProductGroupId(long value)
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
