

using Infrastructure.Common.Abstractions;
namespace Infrastructure.AppIdGenerators.Primitives;

internal sealed class SnowflakeGenerator(IdGen.IIdGenerator<long> _gen): IPrimitiveTypeIdGenerator<long>
{
    public long Generate()
    {
        return _gen.CreateId();
    }

}
