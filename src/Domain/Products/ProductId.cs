
namespace Domain.Products;
public readonly record struct ProductId
{
    public long Value { get; }

    public static implicit operator long(ProductId productId) => productId.Value;
    public static implicit operator ProductId(long value) => new (value);
    public ProductId(long value)
    {
        if (value <= 0)
            throw new ArgumentException("ProductId is invalid.", nameof(value));

        Value = value;
    }

    public static ProductId Parse(string value)
    {
        if (TryParse(value, out var id))
            return id;
        throw new ArgumentException("ProductId is invalid.", nameof(value));
    }
    public static bool TryParse(string value, out ProductId id)
    {
        if (long.TryParse(value, out var brandId))
        {
            id = new ProductId(brandId);
            return true;
        }
        id = new();
        return false;
    }
}
