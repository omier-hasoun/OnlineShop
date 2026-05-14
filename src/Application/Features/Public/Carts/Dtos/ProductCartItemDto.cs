
using Domain.Common.ValueObjects;
using Domain.ProductsGroups.Products;
using Domain.ProductsGroups.ValueObjects;

namespace Application.Features.Public.Carts.Dtos;

public sealed record ProductCartItemDto
{
    public ProductCartItemDto(ProductId id, ProductImage? productImage, string title, Money price, byte? discountPercentage, Money? priceBeforeDiscount )
    {

        ImageThumbnail = productImage?.FileName;
        Title = title;
        DiscountPercentage = discountPercentage;
        Id = id.Value;
        Price = (double)price.Value;
        PriceBeforeDiscount = (double?)priceBeforeDiscount?.Value;
    }

    public long Id { get; }
    public string Title { get; }
    public double Price { get; }
    public byte? DiscountPercentage { get; }
    public double? PriceBeforeDiscount { get; }
    public string? ImageThumbnail { get; }


}
