
namespace Application.Features.Management.ProductGroups.Commands.ArchiveProductGroup;

public sealed record ArchiveProductGroupCommand(long ProductId, bool ResetProductsStock = true) : IRequest<Result<Success>>
{

    internal ProductGroupId ParsedProductId =>
        new (ProductId);
}
