
using Application.Features.Management.ProductGroups.Dtos;

namespace Application.Features.Management.ProductGroups.Queries.GetProductById;

public sealed record GetProductByIdQuery : IRequest<Result<ProductDto>>
{
    public ProductGroupId ProductId { get; init; }
    public GetProductByIdQuery(long productId)
    {
        ProductId = new ProductGroupId(productId);
    }

}
