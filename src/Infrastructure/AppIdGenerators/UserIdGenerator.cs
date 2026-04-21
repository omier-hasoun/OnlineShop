
using Domain.Customers;
using Infrastructure.Common.Abstractions;
using App = Application.Common.Abstractions;
namespace Infrastructure.AppIdGenerators;

internal sealed class UserIdGenerator([FromKeyedServices("GuidV7")] IPrimitiveTypeIdGenerator<Guid> Generator) : App.IIdGenerator<UserId>
{
    public UserId NewId()
    {
        return Generator.Generate();
    }
}
