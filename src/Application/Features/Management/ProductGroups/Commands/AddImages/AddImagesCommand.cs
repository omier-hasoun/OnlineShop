using Application.Common.Dtos;
using Domain.ProductsGroups.Products;

namespace Application.Features.Management.ProductGroups.Commands.AddImages;

public sealed record AddImagesCommand(long ProductGroupId, long ProductId, IReadOnlyCollection<FileUploadDto> Images) : IRequest<Result<Updated>>
{
    internal ProductGroupId ParsedProductGroupId =>
    new(ProductGroupId);

    internal ProductId ParsedProductId =>
    new(ProductId);
}
