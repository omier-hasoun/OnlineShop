using Application.Common.Dtos;

namespace Application.Features.Management.ProductGroups.Commands.AddImages;

public sealed record AddImagesCommand : IRequest<Result<Updated>>
{
    public required List<FileUploadDto> Images { get; init; }
    public required long ProductGroupId { get; init; }
    public required long ProductId { get; init; }
}
