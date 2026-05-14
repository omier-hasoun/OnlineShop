
using System.Net.NetworkInformation;
using Application.Common.ResponseModels;
using Domain.Common.ValueObjects;
using Domain.ProductsGroups.Products;
using Domain.ProductsGroups.ValueObjects;

namespace Application.Features.Management.ProductGroups.Dtos;

public sealed record ProductDto
{


    public ProductDto(ProductId productId, Money? priceBeforeDiscount, Money price, byte? discountPercentage, DateOnly? discountExpiresOn, ProductStatus status,
        int width, int height, int length, int weight, string sku, string slug, string barCode, Dictionary<string, string> specifications, List<ProductImage> images)
    {
        ProductId = productId.Value;
        Price = (double)price.Value;
        PriceBeforeDiscount = (double?)priceBeforeDiscount?.Value;
        DiscountPercentage = discountPercentage;
        DiscountExpiresOn = discountExpiresOn;
        Width = width;
        Height = height;
        Length = length;
        Weight = weight;
        Sku = sku;
        Slug = slug;
        BarCode = barCode;
        Specifications = specifications;
        Status = status.ToString();
        Images = new List<ProductImageDto>(images.Count);
        images.ForEach(image => Images.Add(new(image));
    }

    public long ProductId { get; }
    public double Price { get; }

    public double? PriceBeforeDiscount { get; }
    public byte? DiscountPercentage { get; }
    public DateOnly? DiscountExpiresOn { get; }
    public int Width { get; }
    public int Height { get; }
    public int Length { get; }
    public int Weight { get; }
    public string Sku { get; }
    public string Slug { get; }
    public string BarCode { get; }
    public string Status { get; }

    public Dictionary<string, string> Specifications { get; }

    public List<ProductImageDto> Images { get; set; }
}
