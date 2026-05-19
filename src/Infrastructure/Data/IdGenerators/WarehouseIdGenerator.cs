using Domain.Warehouses;
using App = Application.Common.Abstractions;
namespace Infrastructure.Data.IdGenerators;

internal sealed class WarehouseIdGenerator([FromKeyedServices("Snowflake")] IPrimitiveTypeIdGenerator<long> Generator) : App.IIdGenerator<WarehouseId>
{
    public WarehouseId NewId()
    {
        return new WarehouseId(Generator.Generate());
    }
}
