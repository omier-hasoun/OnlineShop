
using Application.Common.RequestModels;

namespace Application.AdminPanelFeatures.Products.Commands.UpdateVariantImages;

public sealed record UpdateVariantImagesCommand : IRequest<Result<Updated>>
{
    public required List<ProductVariantImageUpload> Images { get; init; }
    public required long Product_Id { get; init; }
    public required long Variant_Id { get; init; }
}
