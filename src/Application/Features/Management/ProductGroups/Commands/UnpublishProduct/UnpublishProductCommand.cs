

using Domain.ProductGroups.Products;

namespace Application.Features.Management.ProductGroups.Commands.UnpublishProduct;

public sealed record UnpublishProductCommand(long ProductGroupId, long ProductId) : IRequest<Result<Success>>
{
    internal ProductId ParsedProductId =>
    new(ProductId);
    internal ProductGroupId ParsedProductGroupId =>
        new(ProductGroupId);
}
