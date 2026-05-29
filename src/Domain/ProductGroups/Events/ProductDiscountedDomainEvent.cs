
namespace Domain.ProductGroups.Events;

public sealed record ProductDiscountedDomainEvent(ProductId Id) : IDomainEvent;
