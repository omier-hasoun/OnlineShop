
using Application.Features.Management.ProductGroups.Dtos;

namespace Application.Features.Management.ProductGroups.Queries.GetProductsGroupById;

public sealed record GetProductsGroupByIdQuery : IRequest<Result<ProductGroupDto>>
{
    public ProductsGroupId ProductId { get; init; }
    public GetProductsGroupByIdQuery(long productId)
    {
        ProductId = new ProductsGroupId(productId);
    }

}
