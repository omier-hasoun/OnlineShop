using Domain.ProductsGroups.ValueObjects;

namespace Application.Common.ResponseModels;

public sealed record ProductImageDto
{
    public string Image { get; init; } = null!;
    public byte SortOrder { get; init; }

    public ProductImageDto(ProductImage image)
    {
        Image = image.FileName;
        SortOrder = image.SortOrder;
        
    }
}
