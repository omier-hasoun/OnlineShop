namespace Domain.Products.Images;

public sealed class ProductImage : BaseEntity
{

    private ProductImage()
    {
    }

    public static ProductImage Create(ProductImageId id, ProductId productId, string extension, byte sortOrder)
    {
        return new()
        {
            Id = id,
            ProductId = productId,
            Extension = extension,
            SortOrder = sortOrder == 0 ? (byte)1 : sortOrder,
        };
    }

    public ProductImageId Id { get; private init; }
    public ProductId ProductId { get; private set; }
    public byte SortOrder { get; private set; }
    public string Extension { get; private set; } = "";
    public string FileName => $"{Id}.{Extension}";
    internal void UpdateSortOrder(byte sortOrder)
    {
        SortOrder = sortOrder;
    }
}
