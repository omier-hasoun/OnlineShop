namespace Domain.Products;

public sealed class ProductImage : BaseEntity
{

    private ProductImage()
    {
    }

    public static ProductImage Create(ProductId productId, byte sortOrder, ProductImageId id = default)
    {
        return new()
        {
            Id = id == default ? new ProductImageId(Guid.CreateVersion7()) : id,
            ProductId = productId,
            SortOrder = sortOrder,
        };
    }
    public ProductImageId Id { get; private init; }
    public ProductId ProductId { get; private set; }
    public byte SortOrder { get; private set; }
}
