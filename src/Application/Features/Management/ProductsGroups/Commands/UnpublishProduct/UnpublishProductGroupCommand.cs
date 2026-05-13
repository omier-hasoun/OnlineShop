
namespace Application.Features.Management.ProductsGroups.Commands.UnpublishProduct;

public sealed record UnpublishProductGroupCommand(long ProductGroupId) : IRequest<Result<Success>>
{
    internal ProductsGroupId ParsedProductGroupId =>
    new(ProductGroupId);
}
