
using Application.Features.Products.Dtos;

namespace Api.Controllers.Products.Responses;

public sealed record ProductListItemResponse
{
    public long Id { get; init; }
    public string Title { get; init; } = null!;
    public double PriceNow { get; init; } 
    public string Brand { get; init; } = null!;
    public float AverageRating { get; init; } 
    public string ImageUrl { get; init; } = null!;
    public byte DiscountPercentage { get; init; }
    public double OriginalPrice { get; init; }

}
