namespace Infrastructure.Common.Abstractions;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(
    IDomainEvent domainEvent,
    CancellationToken ct = default);

}
