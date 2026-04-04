namespace Domain.Products.ProductImages;

public sealed class ProductImage : BaseEntity
{

    private ProductImage()
    {
    }

    public static ProductImage Create(ProductImageId id, ProductId productId, string fileName, byte sortOrder)
    {
        return new()
        {
            Id = id,
            ProductId = productId,
            FileName = fileName,
            SortOrder = sortOrder <= 0 ? (byte)1 : sortOrder,
        };
    }

    public ProductImageId Id { get; private init; }
    public ProductId ProductId { get; private set; }
    public byte SortOrder { get; private set; }
    public string FileName { get; private set; } = null!;
    internal void UpdateSortOrder(byte sortOrder)
    {
        SortOrder = sortOrder;
    }
}
