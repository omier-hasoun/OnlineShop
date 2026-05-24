
using Domain.ProductGroups.Products;

namespace Application.Features.Management.ProductGroups.Commands.ApplyDiscount;

public sealed record ApplyDiscountCommand(long ProductGroupId, long ProductId, DateOnly DiscountExpiresOn, byte DiscountPercentage) 
: IRequest<Result<Success>>
{
    internal ProductGroupId ParsedProductGroupId =>
new(ProductGroupId);

    internal ProductId ParsedProductId =>
    new(ProductId);
}
