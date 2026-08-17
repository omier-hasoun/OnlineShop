namespace Domain.Inventories;

public sealed class Inventory : IAggregateRoot // i want to a composite id in this entity, i cant do that if i inherit AggregateRoot
{
    private Inventory()
    {
        
    }
    private Inventory(WarehouseId warehouseId, ProductId productId, int quantity, int reservedQuantity)
    {
        WarehouseId = warehouseId;
        ProductId = productId;
        StockQuantity = quantity;
        ReservedQuantity = reservedQuantity;
    }

    public static Result<Inventory> Create(WarehouseId warehouseId, ProductId productId, int stockQuantity)
    {
        var validationResult = Result.ValidateAll(
                                () => warehouseId.IsValid(),
                                () => productId.IsValid(),
                                () => ValidateStockQuantity(stockQuantity)
                               );

        if (validationResult.Failed)
            return validationResult.Errors;

        return new Inventory(warehouseId, productId, stockQuantity, reservedQuantity: 0);
    }


    public WarehouseId WarehouseId { get; private init; }
    public ProductId ProductId { get; private init; }
    public int StockQuantity { get; private set; }
    public int ReservedQuantity { get; private set; }
    public Warehouse Warehouse { get; private set; } = null!;

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    private readonly List<IDomainEvent> _domainEvents = [];

    public Result<Success> Remove(int quantity)
    {
        if (quantity < 1)
            return DomainErrors.Inventories.QuantityInvalid;
        
        if (quantity > StockQuantity)
            return DomainErrors.Inventories.InSufficientStock;

        StockQuantity -= quantity;

        return Result.Success;
    }

    public Result<Success> ReserveQuantity(int quantity)
    {
        var result = Remove(quantity);

        if (result.Failed)
        {
            return result;
        }
        
        ReservedQuantity += quantity;
        return result;
    }

    public void CancelQuantityReservation(int quantity)
    {
        if (ValHelper.IsOutOfRange(quantity, 1, ReservedQuantity))
        {
            return;// ignore invalid inputs
        }
        ReservedQuantity -= quantity;

        StockQuantity += quantity;// back to stock

    }
    public void TakeQuantityFromReserved(int quantity)
    {
        if (ValHelper.IsOutOfRange(quantity, 1, ReservedQuantity))
        {
            return;// ignore invalid inputs
        }

        ReservedQuantity -= quantity;
    }

    public Result<Success> Restock(int stockQuantity)
    {

        var result = ValidateStockQuantity(stockQuantity + StockQuantity);

        if (result.Failed)
        {
            return result;
        }
        StockQuantity += stockQuantity;

        return result;
    }

    public void ResetStock()
    {
        StockQuantity = 0;
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


    private static Result<Success> ValidateStockQuantity(int stockQuantity)
    {
        if (ValHelper.IsOutOfRange(stockQuantity, 0, 1000000))
            return DomainErrors.Inventories.QuantityInvalid;

        return Result.Success; 
    }

}
