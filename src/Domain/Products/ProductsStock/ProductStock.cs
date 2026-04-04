namespace Domain.Products.ProductsStock;

public sealed class ProductStock : BaseEntity
{
    private ProductStock(WarehouseId warehouseId, ProductVariantId productVariantId, int quantity)
    {
        WarehouseId = warehouseId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
    }

    public static Result<ProductStock> Create(WarehouseId warehouseId, ProductVariantId productVariantId, int quantity)
    {

        return new ProductStock(warehouseId, productVariantId, quantity);
    }

    public WarehouseId WarehouseId { get; private init; }
    public ProductVariantId ProductVariantId { get; private init; }
    public int Quantity { get; private set; }

    public Warehouse WarehouseInfo { get; private set; }
}
