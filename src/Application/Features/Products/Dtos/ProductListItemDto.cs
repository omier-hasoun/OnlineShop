
namespace Application.Features.Products.Dtos;

public sealed record ProductListItemDto(long ProductId, string Title, double PriceNow, string Brand, float AverageRating, string? ImagePath, byte? DiscountPercentage, double? OriginalPrice);
