
using Microsoft.AspNetCore.Http;

namespace Application.Common.RequestModels;
public sealed record ProductImageUpload
{
    public required IFormFile File { get; init; }
    public required byte SortOrder { get; init; }
}
