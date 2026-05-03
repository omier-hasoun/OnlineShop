
using Domain.Products.ValueObjects;

namespace Application.Features.Products.Dtos;

public sealed record VariantImageDto
{
    public string FilePath { get; init; } = null!;
    public byte SortOrder { get; init; }

    public static List<VariantImageDto> FromProductVariantImage(IReadOnlyCollection<ProductImage> productImages)
    {
        List<VariantImageDto> imagesDto = new(productImages.Count);
        foreach (var image in productImages )
        {
            imagesDto.Add(new VariantImageDto
            {
                FilePath = image.FileName,
                SortOrder = image.SortOrder
            });
        }
        return imagesDto;
    }

}
