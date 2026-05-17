
using Application.Common.Dtos;
using Domain.ProductsGroups.Products;

namespace Application.Features.Management.ProductGroups.Commands.UpdateImagesSortOrder;

public sealed record UpdateImagesSortOrderCommand(long ProductGroupId, long ProductId, IReadOnlyCollection<ProductImageDto> Images) : IRequest<Result<Updated>>
{
    internal ProductGroupId ParsedProductGroupId =>
    new(ProductGroupId);

    internal ProductId ParsedProductId =>
    new(ProductId);

}
