

namespace Domain.ProductGroups.Events;

public sealed record ProductGroupArchivedDomainEvent(ProductGroupId Id) : IDomainEvent;
