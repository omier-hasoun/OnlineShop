

namespace Domain.ProductStocks;

public readonly record struct ProductStockId
{
    public long Value { get; }

    public ProductStockId(long value)
    {
        if (value <= 0)
            throw new ArgumentException("ProductStockId is invalid.", nameof(value));

        Value = value;
    }

    public static implicit operator long(ProductStockId productStockId) => productStockId.Value;
    public static implicit operator ProductStockId(long value) => new ProductStockId(value);
}
