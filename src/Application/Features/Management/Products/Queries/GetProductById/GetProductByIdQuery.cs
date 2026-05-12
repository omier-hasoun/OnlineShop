
using Application.Features.Management.Products.Dtos;

namespace Application.Features.Management.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery : IRequest<Result<ProductDto>>
{
    public long ProductId { get; init; }
    public GetProductByIdQuery(long productId)
    {
        ProductId = productId;
    }

}
