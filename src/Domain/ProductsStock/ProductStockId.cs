

namespace Domain.ProductsStock;

public readonly record struct ProductStockId
{
    public long Value { get; }

    public ProductStockId(long value)
    {
        Value = value;
    }

    public Result<Success> IsValid(long value)
    {
        if (Value <= 0)
        {
            return DomainErrors.ProductsStock.ProductStockIdInvalid;
        }

        return Result.Success;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
