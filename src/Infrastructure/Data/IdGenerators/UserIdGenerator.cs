
using Domain.Customers;
using Infrastructure.Common.Abstractions;
using App = Application.Common.Abstractions;
namespace Infrastructure.Data.IdGenerators;

internal sealed class UserIdGenerator([FromKeyedServices("GuidV7")] IPrimitiveTypeIdGenerator<Guid> Generator) : App.IIdGenerator<CustomerId>
{
    public CustomerId NewId()
    {
        return Generator.Generate();
    }
}
