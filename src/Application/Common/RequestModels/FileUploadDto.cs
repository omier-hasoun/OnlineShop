
namespace Application.Common.RequestModels;

public sealed record FileUploadDto
{
    public required string FileName { get; init; }
    public required long ContentLength { get; init; }
    public required string MediaType { get; init; }
    public required Stream ContentStream { get; init; }

}
