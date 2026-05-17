namespace Application.Common.Dtos;

public sealed record FileUploadDto
{
    public required string OriginalFileName { get; init; }
    public required string InternalFileName { get; init; }

    public required long ContentLength { get; init; }
    public required string MediaType { get; init; }
    public required Stream ContentStream { get; init; }


}
