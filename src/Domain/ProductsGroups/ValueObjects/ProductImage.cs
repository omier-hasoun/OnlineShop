
namespace Domain.ProductsGroups.ValueObjects;

public sealed record ProductImage
{

    private ProductImage() { }

    public string FileName { get; private init; } = null!;

    public byte SortOrder { get; private init; }


    public static ProductImage Create(string fileName, byte sortOrder)
    {
        return new ProductImage()
        {
            FileName = fileName,
            SortOrder = sortOrder,
        };
    }

    internal ProductImage ChangeSortOrder(byte sortOrder)
    {
        return new ProductImage
        {
            FileName = this.FileName,
            SortOrder = sortOrder

        };
    }
}
