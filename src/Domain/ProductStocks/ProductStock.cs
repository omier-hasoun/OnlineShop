namespace Domain.ProductStocks;

public sealed class ProductStock : AggregateRoot<ProductStockId>
{
    private ProductStock(ProductStockId id, WarehouseId warehouseId, ProductVariantId productVariantId, int quantity, int reservedQuantity)
        : base(id)
    {
        WarehouseId = warehouseId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
        ReservedQuantity = reservedQuantity;
    }

    public static Result<ProductStock> Create(ProductStockId id, WarehouseId warehouseId, ProductVariantId productVariantId, int quantity)
    {

        return new ProductStock(id, warehouseId, productVariantId, quantity, 0);
    }

    public WarehouseId WarehouseId { get; private init; }
    public ProductVariantId ProductVariantId { get; private init; }
    public int Quantity { get; private set; }

    public int ReservedQuantity { get; private set; }

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

    public Result<Success> AddStock(int quantity)
    {
        if (quantity <= 0)
        {
        }
        return Result.Success;
    }

}
