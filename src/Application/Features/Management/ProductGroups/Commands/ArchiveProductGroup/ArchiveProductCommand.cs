

using Domain.ProductGroups.Products;

namespace Application.Features.Management.ProductGroups.Commands.ArchiveProductGroup;

public sealed record ArchiveProductCommand(long ProductGroupId, long ProductId) : IRequest<Result<Success>>
{
    internal ProductGroupId ParsedProductGroupId =>
    new(ProductGroupId);
    internal ProductId ParsedProductId =>
        new(ProductId);
}

