
using Domain.ProductsGroups.Products;

namespace Application.Features.Management.ProductGroups.Commands.RemoveImages;

public sealed record RemoveImagesCommand(long ProductGroupId, long ProductId, List<string> FileNames) : IRequest<Result<Deleted>>
{
    internal ProductGroupId ParsedProductGroupId =>
new(ProductGroupId);

    internal ProductId ParsedProductId =>
    new(ProductId);
}
