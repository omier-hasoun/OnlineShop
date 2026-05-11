using Application.Common.RequestModels;

namespace Api.Requests;

public sealed record UpdateProductImagesRequest
{
    public required List<ProductImageUpload> Images { get; init; } 
}
