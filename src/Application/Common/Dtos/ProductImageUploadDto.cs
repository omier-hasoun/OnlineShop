

namespace Application.Common.Dtos;
public sealed record ProductImageUploadDto
{
    public required FileUploadDto File { get; init; }
    public required byte SortOrder { get; init; }
}
