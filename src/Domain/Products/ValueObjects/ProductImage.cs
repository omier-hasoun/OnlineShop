
namespace Domain.Products.ValueObjects;

public sealed record ProductImage
{

    private ProductImage() { }

    public string FilePath { get; private init; } = null!;

    public byte SortOrder { get; private init; }


    public static Result<ProductImage> From(string filePath, byte sortOrder)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            return DomainErrors.Products.InvalidImageFilePath;
        }

        return new ProductImage()
        {
            FilePath = filePath,
            SortOrder = sortOrder,
        };
    }
}
