
namespace Application.Features.Management.ProductGroups.Commands.UnpublishProduct;

public sealed record UnpublishProductGroupCommand(long ProductGroupId) : IRequest<Result<Success>>
{
    internal ProductGroupId ParsedProductGroupId =>
    new(ProductGroupId);
}
