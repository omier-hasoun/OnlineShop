namespace Infrastructure.IdGenerators;

public sealed class GuidVersion7IdProvider : IIdProvider<Guid>
{
    public Guid GetNewId()
    {
        return Guid.CreateVersion7();
    }
}
