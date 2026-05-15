using Application.Common.RequestModels;

namespace Application.Features.Management.ProductGroups.Commands.UpdateProductImages;

public sealed record UpdateProductImagesCommand : IRequest<Result<Updated>>
{
    public required List<ProductImageUploadDto> Images { get; init; }
    public required long ProductGroupId { get; init; }
    public required long ProductId { get; init; }
}
