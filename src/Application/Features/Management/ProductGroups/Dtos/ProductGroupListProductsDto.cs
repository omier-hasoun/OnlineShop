
using Domain.Common.ValueObjects;
using Domain.ProductGroups.Products;
using Domain.ProductGroups.ValueObjects;

namespace Application.Features.Management.ProductGroups.Dtos;

public sealed record ProductGroupListProductsDto
{
    public long Id { get; init; }
    public double Price { get; init; }

    public double? PriceBeforeDiscount { get; init; }
    public byte? DiscountPercentage { get; init; }
    public DateOnly? DiscountExpiresOn { get; }

    public string? Image { get; init; }
    public string Status { get; init; } = null!;

    public bool HasActiveDiscount { get; init; }


    public ProductGroupListProductsDto(ProductId id, Money price, bool hasActiveDiscount, byte? discountPercentage, Money? priceBeforeDiscount, DateOnly? discountExpiresOn, ProductState status, ProductImage? image)
    {
        Id = id.Value;

        Status = status.ToString();

        Price = (double)price.Value;

        HasActiveDiscount = hasActiveDiscount;

        DiscountExpiresOn = hasActiveDiscount ? discountExpiresOn : null;

        PriceBeforeDiscount = hasActiveDiscount ? (double)priceBeforeDiscount!.Value : null;

        DiscountPercentage = hasActiveDiscount ? discountPercentage : null;

        Image = image?.FileName;

    }
}
