
namespace Domain.ProductsGroups;
public readonly record struct ProductsGroupId
{
    public long Value { get; }
    public ProductsGroupId(long value)
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
