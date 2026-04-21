
namespace Infrastructure.AppIdGenerators.Primitives;

using Infrastructure.Common.Abstractions;

internal sealed class GuidV7Generator : IPrimitiveTypeIdGenerator<Guid>
{
    public Guid Generate()
    {
        return Guid.CreateVersion7(TimeService.UtcNow);

    }
}
