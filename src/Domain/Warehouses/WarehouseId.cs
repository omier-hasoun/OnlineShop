
namespace Domain.Warehouses;

public readonly record struct WarehouseId
{
    public long Value { get; init; }
    public WarehouseId(long value)
    {
        if(value < 1) throw new ArgumentOutOfRangeException("value");

        Value = value;
    }

    public static implicit operator long(WarehouseId id) => id.Value;
    public static implicit operator WarehouseId(long value) => new(value);

    public static WarehouseId Parse(string value)
    {
        if (TryParse(value, out var id))
            return id;
        throw new ArgumentException("ProductVariantId is invalid.", nameof(value));
    }
    public static bool TryParse(string value, out WarehouseId id)
    {
        if (long.TryParse(value, out var brandId))
        {
            id = new WarehouseId(brandId);
            return true;
        }
        id = new();
        return false;
    }
}
