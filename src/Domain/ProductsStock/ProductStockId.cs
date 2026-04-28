

namespace Domain.ProductsStock;

public readonly record struct ProductStockId
{
    public long Value { get; }

    internal ProductStockId(long value)
    {
        Value = value;
    }

    public static implicit operator long(ProductStockId productStockId) => productStockId.Value;
    public static implicit operator ProductStockId(long value) => new ProductStockId(value);

    public static Result<ProductStockId> From(long value)
    {
        if (value <= 0)
        {
            return new ProductStockId(value);
        }

        return DomainErrors.Categories.CategoryIdInvalid;
    }
}
