using Domain.Carts;
using Domain.Common.Entities.Addresses;
using App = Application.Common.Abstractions;
namespace Infrastructure.Data.IdGenerators;

internal sealed class AddressIdGenerator([FromKeyedServices("Snowflake")] IPrimitiveTypeIdGenerator<long> Generator) : App.IIdGenerator<AddressId>
{
    public AddressId NewId()
    {
        return new AddressId(Generator.Generate());
    }
}
