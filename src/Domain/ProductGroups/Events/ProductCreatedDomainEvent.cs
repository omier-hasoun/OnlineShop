
namespace Domain.ProductGroups.Events;

public sealed record ProductCreatedDomainEvent(ProductId ProductId)    
: IDomainEvent;
