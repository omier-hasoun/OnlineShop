
namespace Domain.Warehouses;

public readonly record struct WarehouseId
{
    public long Value { get; }
    public WarehouseId(long value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value <= 0)
        {
            return DomainErrors.Warehouses.WarehouseIdInvalid;
        }

        return Result.Success;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
