
using Domain.ProductsGroups.Products;

namespace Application.Features.Management.ProductGroups.Commands.PublishProduct;

public sealed record PublishProductCommand(long ProductGroupId, long ProductId) : IRequest<Result<Success>>
{
    internal ProductGroupId ParsedProductGroupId =>
    new(ProductGroupId);

    internal ProductId ParsedProductId =>
    new(ProductId);
}
