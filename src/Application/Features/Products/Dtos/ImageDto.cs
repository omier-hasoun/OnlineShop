
using Domain.Products.ValueObjects;

namespace Application.Features.Products.Dtos;

public sealed record ImageDto
{
    public string Image { get; init; } = null!;
    public byte SortOrder { get; init; }

    public ImageDto(ProductImage image)
    {
        Image = image.FileName;
        SortOrder = image.SortOrder;
        
    }
}
