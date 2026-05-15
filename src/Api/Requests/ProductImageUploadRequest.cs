
namespace Api.Requests;

public sealed record ProductImageUploadRequest
{
    public required IFormFile File { get; init; }
    public required byte SortOrder { get; init; }
}
