

namespace Domain.ProductsStock;

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

    public static ProductStockId Parse(string value)
    {
        if (TryParse(value, out var id))
            return id;
        throw new ArgumentException("ProductStockId is invalid.", nameof(value));
    }
    public static bool TryParse(string value, out ProductStockId id)
    {
        if (long.TryParse(value, out var brandId))
        {
            id = new ProductStockId(brandId);
            return true;
        }
        id = new();
        return false;
    }
}
