
using IdGen;
namespace Infrastructure.IdGenerators;

public sealed class SnowflakeIdProvider(IIdGenerator<long> generator) : IIdProvider<long>
{
    public long GetNewId()
    {
        return generator.CreateId();
    }
}
