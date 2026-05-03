
namespace Domain.Products.ValueObjects;

public sealed record ProductImage
{

    private ProductImage() { }

    public string FileName { get; private init; } = null!;

    public byte SortOrder { get; private set; }


    public static Result<ProductImage> From(string fileName, byte sortOrder)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return DomainErrors.Products.InvalidImageFilePath;
        }

        return new ProductImage()
        {
            FileName = fileName,
            SortOrder = sortOrder,
        };
    }

    internal void ChangeSortOrder(byte sortOrder)
    {
        SortOrder = sortOrder;
    }
}
