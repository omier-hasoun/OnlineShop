namespace Infrastructure.Common.Abstractions;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(
    IReadOnlyCollection<IDomainEvent> domainEvents,
    CancellationToken ct = default);

}
