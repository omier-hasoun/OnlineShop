
using Application.Common.InternalModels;
using Domain.ProductsGroups.Products;

namespace Application.Features.Public.Carts.Commands.AddCartItem;

public sealed record AddCartItemCommand(CartIdentity CartIdentity, long ProductId, short Quantity) : IRequest<Result<long>>
{
    public ProductId ParsedProductId { get; init; } = new ProductId(ProductId);

}
