using Application.Common.RequestModels;

namespace Application.AdminPanelFeatures.Products.Commands.CreateVariant;

public sealed record CreateVariantCommand 
(
    ProductId ProductId,
    decimal Price,
    int Width,
    int Height,
    int Length,
    int Weight,
    string Sku,
    string Slug,
    string BarCode,
    IReadOnlyList<ProductVariantImageUpload> Images,
    IReadOnlyDictionary<string, string> Specifications
) : IRequest<Result<long>>;

