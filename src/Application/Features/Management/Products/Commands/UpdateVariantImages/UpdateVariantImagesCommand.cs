using Application.Common.RequestModels;

namespace Application.Features.Management.Products.Commands.UpdateVariantImages;

public sealed record UpdateVariantImagesCommand : IRequest<Result<Updated>>
{
    public required List<ProductImageUpload> Images { get; init; }
    public required long ProductId { get; init; }
    public required long VariantId { get; init; }
}
