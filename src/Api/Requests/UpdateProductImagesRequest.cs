using Application.Common.RequestModels;

namespace Api.Requests;

public sealed record UpdateProductImagesRequest
{
    public required List<ProductImageUploadRequest> Images { get; init; } 
}
