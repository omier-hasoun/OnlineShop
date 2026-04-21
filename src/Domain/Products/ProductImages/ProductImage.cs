namespace Domain.Products.ProductImages;

public sealed class ProductImage : BaseEntity<ProductImageId>
{

    private ProductImage(ProductImageId id, ProductId productId, ProductVariantId? productVariantId, string fileName, byte sortOrder, int fileSize) : base(id)
    {
        ProductId = productId;
        ProductVariantId = productVariantId;
        FileName = fileName;
        SortOrder = sortOrder;
        FileSize = fileSize;
    }

    public static Result<ProductImage> Create(ProductImageId id, ProductId productId, ProductVariantId? productVariantId, string fileName, byte sortOrder, int fileSize)
    {
        return new ProductImage(id, productId, productVariantId, fileName, sortOrder, fileSize);
    }

    public ProductId ProductId { get; private init; }
    public ProductVariantId? ProductVariantId { get; private init; }

    public byte SortOrder { get; private set; }
    public string FileName { get; private set; } = null!;
    public int FileSize { get; private set; }

    internal void UpdateSortOrder(byte sortOrder)
    {
        SortOrder = sortOrder;
    }
}
