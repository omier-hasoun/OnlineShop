
using Domain.Common.ValueObjects;
using Domain.Products.ValueObjects;

namespace Application.Features.Products.Dtos;
/// <summary>
/// This Dto used to for listing Products when requested
/// </summary>
/// <param name="VariantId"></param>
/// <param name="ProductId"></param>
/// <param name="Title"></param>
/// <param name="PriceNow"></param>
/// <param name="Brand"></param>
/// <param name="AverageRating"></param>
/// <param name="Images"></param>
/// <param name="DiscountPercentage"></param>
/// <param name="OriginalPrice"></param>
public sealed record ProductListItemDto
{
    public ProductId Id { get; init; }
    public string Title { get; init; } = null!;
    public Money PriceNow { get; init; } = null!;
    public string Brand { get; init; } = null!;
    public ProductAverageRating AverageRating { get; init; } = null!;
    public ProductImage Image { get; init; } = null!;
    public byte DiscountPercentage { get; init; }
    public Money OriginalPrice { get; init; } = null!;

    public ProductListItemDto()
    {
        
    }
}
