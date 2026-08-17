using Application.Features.Public.Carts.Dtos;

namespace Application.Features.Public.Carts.Queries.GetCart;

internal sealed class GetCartQueryHandler(IAppDbContext context) : IRequestHandler<GetCartQuery, Result<CartDto>>
{
    public async Task<Result<CartDto>> Handle(GetCartQuery request, CancellationToken ct)
    {
        var cartIdentity = request.CartIdentity;

        var baseQuery = context.Carts.AsNoTracking();

        if (cartIdentity.IsUser)
        {
            baseQuery = baseQuery.Where(cart => cart.UserId == cartIdentity.UserId);
        }
        else
        {
            baseQuery = baseQuery.Where(cart => cart.GuestId == cartIdentity.GuestId);
        }


        var cartDto = await baseQuery
            .Select(c => new CartDto(
                c.Id,

                c.Items.Select(i => new CartItemDto(
                    i.Id,
                    i.Quantity,

                    context.Products
                        .Where(p => p.Id == i.ProductId)
                        .Select(p => new ProductCartItemDto(
                            p.Id,
                            p.Images.FirstOrDefault(),

                            context.ProductGroups
                                .Where(pg => pg.Id == p.ProductGroupId)
                                .Select(pg => pg.Title)
                                .First(),

                            p.OriginalPrice,
                            p.DiscountPercentage,
                            p.DiscountPrice
                        ))
                        .FirstOrDefault()

                )).ToList()
            )).FirstOrDefaultAsync(ct);

        if (cartDto is null)
            return ApplicationErrors.NotFound.Cart;


        return cartDto;

    }
}
