namespace Application.Common.RequestModels;

public sealed record UploadedFile
{
    public required string FileName { get; init; }
    public required string MediaType { get; init; }
    public required long SizeInBytes { get; init; }

    public required Stream Content { get; init; }
}
