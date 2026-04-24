
namespace Domain.Common.Abstractions;

public abstract class AggregateRoot<TId> : BaseEntity<TId> where TId : struct, IEquatable<TId>
{
    protected AggregateRoot(TId Id) : base(Id)
    {
    }
    protected AggregateRoot() : base()
    {
        
    }
}
