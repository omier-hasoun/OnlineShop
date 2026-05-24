namespace Domain.Inventories;

public sealed class Inventory : IAggregateRoot // i want to a composite id in this entity, i cant do that if i inherit AggregateRoot
{
    private Inventory(WarehouseId warehouseId, ProductId productId, int quantity, int reservedQuantity)
    {
        WarehouseId = warehouseId;
        ProductId = productId;
        Quantity = quantity;
        ReservedQuantity = reservedQuantity;
    }

    public static Result<Inventory> Create(WarehouseId warehouseId, ProductId productId, int quantity)
    {
        var validationResult = Result.ValidateAll(
                                () => warehouseId.IsValid(),
                                () => productId.IsValid(),
                                () => ValidateQuantity(quantity)
                               );

        if (validationResult.Failed)
            return validationResult.Errors;

        return new Inventory(warehouseId, productId, quantity, 0);
    }

    public WarehouseId WarehouseId { get; private init; }
    public ProductId ProductId { get; private init; }
    public int Quantity { get; private set; }
    public int ReservedQuantity { get; private set; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    private readonly List<IDomainEvent> _domainEvents = [];

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


    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        if (domainEvent is null)
            return;

        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }


    private static Result<Success> ValidateQuantity(int quantity)
    {
        if (quantity <= 0 || quantity > 100000)
            return DomainErrors.Inventories.QuantityOutOfRange;

        return Result.Success; 
    }

}
