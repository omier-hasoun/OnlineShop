
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
public sealed record ProductListItemViewDto
{
    public long Id { get; init; }
    public string Title { get; init; } = null!;
    public decimal PriceNow { get; init; } 
    public string Brand { get; init; } = null!;
    public float AverageRating { get; init; }
    public List<ProductImage> Images { get; init; } = null!;
    public byte DiscountPercentage { get; init; }
    public decimal OriginalPrice { get; init; } 

    public ProductListItemViewDto()
    {
        
    }
}
