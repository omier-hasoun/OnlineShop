
using Application.Features.Management.ProductsGroups.Dtos;

namespace Application.Features.Management.ProductsGroups.Queries.GetProductById;

public sealed record GetProductByIdQuery : IRequest<Result<ProductDto>>
{
    public ProductsGroupId ProductId { get; init; }
    public GetProductByIdQuery(long productId)
    {
        ProductId = new ProductsGroupId(productId);
    }

}
