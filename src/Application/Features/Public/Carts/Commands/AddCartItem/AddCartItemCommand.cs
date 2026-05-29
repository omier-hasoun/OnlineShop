
using Application.Common.Dtos;
using Domain.ProductGroups.Products;

namespace Application.Features.Public.Carts.Commands.AddCartItem;

public sealed record AddCartItemCommand(UserIdentity CartIdentity, long ProductId, short Quantity) : IRequest<Result<long>>
{
    internal ProductId ParsedProductId =>
    new(ProductId);

}
