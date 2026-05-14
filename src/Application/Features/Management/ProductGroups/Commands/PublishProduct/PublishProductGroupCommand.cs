
using Domain.ProductsGroups.Products;

namespace Application.Features.Management.ProductGroups.Commands.PublishProduct;

public sealed record PublishProductGroupCommand(long ProductGroupId) : IRequest<Result<Success>>
{
    internal ProductsGroupId ParsedProductsGroupId =>
    new(ProductGroupId);
}
