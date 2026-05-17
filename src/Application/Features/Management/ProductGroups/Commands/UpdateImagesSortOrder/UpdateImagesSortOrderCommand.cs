
using Application.Common.Dtos;
using Domain.ProductsGroups.Products;

namespace Application.Features.Management.ProductGroups.Commands.UpdateImagesSortOrder;

public sealed record UpdateImagesSortOrderCommand(long ProductGroupId, long ProductId, List<ProductImageDto> Images) : IRequest<Result<Updated>>
{
    public ProductId ParsedProductId { get; private set; } = new(ProductId);
    public ProductGroupId ParsedProductGroupId { get; private set; } = new(ProductGroupId);


    
}
