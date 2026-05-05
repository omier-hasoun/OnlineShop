
namespace Application.Features.Products.Dtos;

public sealed record ProductVariantDto
{
    public double PriceNow { get; init; }
    public double? PriceBeforeDiscount { get; init; }
    public byte? DiscountPercentage { get; init; }
    public IReadOnlyCollection<ImageDto> Images { get; init; } = null!;
    public IReadOnlyDictionary<string, string> Specifications { get; init; } = null!;

    public string Slug { get; init; } = null!;

}
