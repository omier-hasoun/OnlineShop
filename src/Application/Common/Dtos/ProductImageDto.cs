using Domain.ProductGroups.ValueObjects;

namespace Application.Common.Dtos;

public sealed record ProductImageDto
{
    public string Image { get; init; } 
    public byte SortOrder { get; init; }

    public ProductImageDto(string image, byte SortOrder)
    {
        Image = image;
        this.SortOrder = SortOrder;
    }


    internal ProductImageDto(ProductImage image) : this(image.FileName, image.SortOrder)
    {

    }

    public static List<ProductImageDto> FromProductImages(List<ProductImage> images)
    {
        List<ProductImageDto> imagesDto = new(images.Count);

        images.ForEach(image =>
        {
            imagesDto.Add(new ProductImageDto(image));
        });

        return imagesDto;
    }

    public static List<ProductImage> ToProductImages(IReadOnlyCollection<ProductImageDto> imagesDto)
    {
        List<ProductImage> images = new(imagesDto.Count);

        foreach (var item in imagesDto)
        {
            images.Add(ProductImage.Create(item.Image, item.SortOrder));
        }
        return images;
    }

}
