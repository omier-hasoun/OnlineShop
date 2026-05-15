
using Microsoft.AspNetCore.Http;

namespace Application.Common.RequestModels;
public sealed record ProductImageUploadDto
{
    public required FileUploadDto File { get; init; }
    public required byte SortOrder { get; init; }
}
