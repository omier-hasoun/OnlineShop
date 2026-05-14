using Application.Features.Public.ProductsGroups.Dtos;

namespace Application.Features.Public.ProductsGroups.Queries.GetProductsGroupById;

public sealed record GetProductsGroupByIdQuery(long productId) : IRequest<Result<ProductsGroupDto>>
{
    public ProductsGroupId ProductId { get; } = new (productId);

}
