
namespace Domain.Common.Abstractions;

public abstract class BaseEntity<TId> : IEntity where TId : struct, IEquatable<TId>
{
    public TId Id { get; init; }
    protected BaseEntity(TId Id)
    {
        this.Id = Id;
    }
    protected BaseEntity()
    {

    }
}
