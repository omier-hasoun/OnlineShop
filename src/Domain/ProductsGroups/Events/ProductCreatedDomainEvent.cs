
namespace Domain.ProductsGroups.Events;

public sealed record ProductCreatedDomainEvent(ProductId ProductId)    
: DomainEvent;
