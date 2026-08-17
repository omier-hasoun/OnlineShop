using Application.Features.Public.Products.Dtos;

namespace Application.Features.Public.Products.Queries.GetProductGroupById;

public sealed record GetProductGroupByIdQuery(long productId) : IRequest<Result<ProductGroupDto>>
{
    public ProductGroupId ProductGroupId { get; } = new(productId);

}
