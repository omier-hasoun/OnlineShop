
using Application.Common.Extensions;

using Domain.Services.Checkout;

namespace Application.Features.Public.Checkout.Queries.ReviewOrderDetails;

internal sealed class ReviewOrderDetailsQueryHandler(IAppDbContext context, CheckoutService checkoutService) : IRequestHandler<ReviewOrderDetailsQuery, Result<OrderPreview>>
{
    public async Task<Result<OrderPreview>> Handle(ReviewOrderDetailsQuery request, CancellationToken ct)
    {
        //var identity = request.Identity;

        //var items = await context.Carts.AsNoTracking()
        //                        .UserCartQuery(identity)
        //                        .Join(context.CartItems, c => c.Id, i => i.CartId, (c, i) => new { c, i })
        //                        .Join(context.Products, x => x.i.ProductId, p => p.Id, (ci, p) => new { ci.c, ci.i, p })
        //                        .Join(context.ProductGroups, x => x.p.ProductGroupId, pg => pg.Id, (cip, pg) => new { cip.c, CartItem = cip.i, Product= cip.p, Group = pg })
        //                        .Select(x => new OrderLineDetails
        //                        (
        //                            x.CartItem.Quantity,
        //                            x.Product,
        //                            x.Group
        //                        ))
        //                        .ToListAsync(ct);

        //if (items is null)
        //    return ApplicationErrors.Validation.CartIsEmpty;

        //var result = checkoutService.CreateOrder(items);

        //if (result.Failed)
        //    return result.Errors;

        //return result.Value;
        throw new NotImplementedException();

    }
}
