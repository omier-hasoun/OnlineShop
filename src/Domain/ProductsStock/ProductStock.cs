namespace Domain.ProductsStock;

public sealed class ProductStock : IEntity // i want to a composite id in this entity, i cant do that if i inherit BaseEntity 
{
    private ProductStock(WarehouseId warehouseId, ProductId productId, int quantity, int reservedQuantity)
    {
        WarehouseId = warehouseId;
        ProductId = productId;
        Quantity = quantity;
        ReservedQuantity = reservedQuantity;
    }

    public static ProductStock Create(WarehouseId warehouseId, ProductId productId, int quantity)
    {

        return new ProductStock(warehouseId, productId, quantity, 0);
    }

    public WarehouseId WarehouseId { get; private init; }
    public ProductId ProductId { get; private init; }
    public int Quantity { get; private set; }
    public int ReservedQuantity { get; private set; }

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    private readonly List<DomainEvent> _domainEvents = [];

    public Result<Success> ReserveItem(short quantity)
    {
        if (quantity <= 0)
        {

        }
        if (quantity > Quantity)
        {

        }


        return Result.Success;
    }

    public Result<Success> ReleaseReservedItem(int quantity)
    {
        if (quantity <= 0)
        {
        }
        if (quantity > ReservedQuantity)
        {
        }
        return Result.Success;
    }
    
    public Result<Success> DeductReservedItem(int quantity)
    {
        if (quantity <= 0)
        {
        }
        if (quantity > ReservedQuantity)
        {
        }
        return Result.Success;
    }

    public Result<Success> Restock(int quantity)
    {
        if (quantity <= 0)
        {
        }
        return Result.Success;
    }


    public void AddDomainEvent(DomainEvent domainEvent)
    {
        if (domainEvent is null)
            return;

        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

}
