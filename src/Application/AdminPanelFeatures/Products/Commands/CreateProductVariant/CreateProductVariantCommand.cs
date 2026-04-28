
using Domain.Products.ValueObjects;

namespace Application.AdminPanelFeatures.Products.Commands.CreateProductVariant;

public sealed record CreateProductVariantCommand
(
    string ProductId,
    decimal Price,
    int width,
    int height,
    int length,
    int weight,
    string Slug,
    string BarCode,
    IReadOnlyCollection<ProductImage> Images,
    IReadOnlyDictionary<string, string>? Specifications
) : IRequest<long>;

