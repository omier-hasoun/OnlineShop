
using Application.Common.Extensions;
using Application.Features.Public.Checkout.Dtos;
using Domain.Common.ValueObjects;
using Domain.Services;

namespace Application.Features.Public.Checkout.Queries.ReviewOrderDetails;

internal sealed class ReviewOrderDetailsQueryHandler(IAppDbContext context) : IRequestHandler<ReviewOrderDetailsQuery, Result<OrderPreviewDto>>
{
    public async Task<Result<OrderPreviewDto>> Handle(ReviewOrderDetailsQuery request, CancellationToken ct)
    {
        //var identity = request.Identity;

        //var items  = await context.Carts.AsNoTracking()
        //                        .GetUserCart(identity)
        //                        .Join(context.CartItems, c => c.Id, i => i.CartId, (c, i) => new { c, i })
        //                        .Join(context.Products, x => x.i.ProductId, p => p.Id, (ci, p) => new { ci.c, ci.i, p })
        //                        .Join(context.ProductGroups, x => x.p.ProductGroupId, pg => pg.Id, (cip, pg) => new { cip.c, cip.i, cip.p, pg })
        //                        .Select(x => new OrderItemPreviewDto(
        //                                        x.p.Id,
        //                                        x.p.Images.FirstOrDefault(),
        //                                        x.pg.Title,
        //                                        x.p.HasActiveDiscount,
        //                                        x.p.HasActiveDiscount ? x.p.DiscountPercentage : null,
        //                                        x.p.HasActiveDiscount ? x.p.OriginalPrice : null,
        //                                        x.p.HasActiveDiscount ? x.p.PriceAfterDiscount : null,
        //                                        x.p.CurrentPrice,
        //                                        x.i.Quantity     
        //                        ))
        //                        .ToListAsync(ct);

        //if (items is null)
        //    return ApplicationErrors.Validation.CartIsEmpty;


        ////var itemsSubtotal = Money.Create(items.Sum(x => x.TotalPrice));
        ////var shippingCost = new ShippingCostCalculator().Calculate(itemsSubtotal);
        ////var orderTotal = itemsSubtotal + shippingCost;

        //return new OrderPreviewDto(items, itemsSubtotal, shippingCost, orderTotal);

        throw new NotImplementedException();

    }
}
