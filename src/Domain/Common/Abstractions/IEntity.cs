
namespace Domain.Common.Abstractions;

public interface IEntity
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents { get;}

}
