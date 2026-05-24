
using Domain.ProductGroups.Products;

namespace Application.Features.Management.ProductGroups.Commands.PublishProduct;

public sealed record PublishProductGroupCommand(long ProductGroupId) : IRequest<Result<Success>>
{
    internal ProductGroupId ParsedProductsGroupId =>
    new(ProductGroupId);
}
