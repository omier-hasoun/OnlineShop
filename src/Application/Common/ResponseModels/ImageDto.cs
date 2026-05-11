using Domain.Products.ValueObjects;

namespace Application.Common.ResponseModels;

public sealed record ImageDto
{
    public string Image { get; init; } = null!;
    public byte Sort_Order { get; init; }

    public ImageDto(ProductImage image)
    {
        Image = image.FileName;
        Sort_Order = image.SortOrder;
        
    }
}
