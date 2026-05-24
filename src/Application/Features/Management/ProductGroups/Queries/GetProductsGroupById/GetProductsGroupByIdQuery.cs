
using Application.Features.Management.ProductGroups.Dtos;

namespace Application.Features.Management.ProductGroups.Queries.GetProductsGroupById;

public sealed record GetProductsGroupByIdQuery : IRequest<Result<ProductGroupDto>>
{
    public ProductGroupId ProductGroupId { get; init; }
    public GetProductsGroupByIdQuery(long productGroupId)
    {
        ProductGroupId = new ProductGroupId(productGroupId);
    }

}
