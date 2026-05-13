
namespace Application.Features.Management.ProductGroups.Commands.ArchiveProductGroup;

public sealed record ArchiveProductGroupCommand(long ProductId) : IRequest<Result<Success>>
{

    internal ProductGroupId ParsedProductId =>
        new (ProductId);
}
