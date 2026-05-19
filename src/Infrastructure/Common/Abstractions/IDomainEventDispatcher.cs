namespace Infrastructure.Common.Abstractions;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(
    IReadOnlyCollection<DomainEvent> domainEvents,
    CancellationToken ct = default);

}
