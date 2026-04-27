using Domain.Common.Exceptions;

namespace Domain.Products.ValueObjects;

public sealed record ProductImage
{

    private ProductImage() { } // optional but safe
    public ProductImage(string filePath, byte sortOrder)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(filePath);

        if (ValidationHelper.IsOutOfRange(sortOrder, 1, 200))
        {
            throw new InvalidImageSortOrderException();
        }
        

        FilePath = filePath;
        SortOrder = sortOrder;
    }
    public string FilePath { get; } = null!;

    public byte SortOrder { get; } 
}
