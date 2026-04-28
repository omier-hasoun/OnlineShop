
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

    public static Result<WarehouseId> From(long value)
    {
        if (value <= 0)
        {
            return new WarehouseId(value);
        }

        return DomainErrors.Warehouses.WarehouseIdInvalid;
    }
}
