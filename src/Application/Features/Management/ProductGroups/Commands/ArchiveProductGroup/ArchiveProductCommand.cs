

using Domain.ProductsGroups.Products;

namespace Application.Features.Management.ProductGroups.Commands.ArchiveProductGroup;

public sealed record ArchiveProductCommand(long ProductGroupId, long ProductId) : IRequest<Result<Success>>
{
    internal ProductsGroupId ParsedProductGroupId =>
    new(ProductGroupId);
    internal ProductId ParsedProductId =>
        new(ProductId);
}

