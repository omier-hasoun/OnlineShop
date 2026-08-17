
using Domain.Common.ValueObjects;
using Domain.ProductGroups.Products;
using Domain.ProductGroups.ValueObjects;

namespace Application.Features.Management.ProductGroups.Dtos;

public sealed record ProductListItemDto
{
    public long Id { get; init; }
    public double Price { get; init; }

    public double? PriceAfterDiscount { get; init; }
    public byte? DiscountPercentage { get; init; }
    public DateOnly? DiscountExpiresOn { get; }
    public bool HasActiveDiscount { get; init; }

    public string? Image { get; init; }
    public string Status { get; init; } = null!;

    public ProductInventoryDto Inventory { get; init; }

    public ProductListItemDto(ProductId id, Money price, bool hasActiveDiscount, byte? discountPercentage,
        Money? priceAfterDiscount, DateOnly? discountExpiresOn, ProductState status, ProductImage? image, ProductInventoryDto inventory)
    {
        Id = id.Value;

        Status = status.ToString();

        Price = (double)price.Value;

        HasActiveDiscount = hasActiveDiscount;
        Inventory = inventory;
        DiscountExpiresOn = hasActiveDiscount ? discountExpiresOn : null;

        PriceAfterDiscount = hasActiveDiscount ? (double)priceAfterDiscount!.Value : null;

        DiscountPercentage = hasActiveDiscount ? discountPercentage : null;

        Image = image?.FileName;

    }
}
