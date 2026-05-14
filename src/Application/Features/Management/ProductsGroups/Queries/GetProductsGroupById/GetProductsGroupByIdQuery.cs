
using Application.Features.Management.ProductsGroups.Dtos;

namespace Application.Features.Management.ProductsGroups.Queries.GetProductsGroupById;

public sealed record GetProductsGroupByIdQuery : IRequest<Result<ProductsGroupDto>>
{
    public ProductsGroupId ProductId { get; init; }
    public GetProductsGroupByIdQuery(long productId)
    {
        ProductId = new ProductsGroupId(productId);
    }

}
