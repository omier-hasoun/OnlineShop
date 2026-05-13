using Application.Features.Public.Products.Dtos;

namespace Application.Features.Public.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(long productId) : IRequest<Result<ProductDto>>
{
    public ProductsGroupId ProductId { get; } = new (productId);

}
