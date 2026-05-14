

using Domain.Carts;
using Domain.Carts.CartItems;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Application.Features.Public.Carts.Commands.AddCartItem;

internal sealed class AddCartItemCommandHandler(IAppDbContext context, IIdGenerator<CartId> cartIdGen, IIdGenerator<CartItemId> cartItemIdGen)
: IRequestHandler<AddCartItemCommand, Result<long>>
{
    public async Task<Result<long>> Handle(AddCartItemCommand request, CancellationToken ct)
    {
        var cartIdentity = request.CartIdentity;
        Cart? cart;

        if (cartIdentity.IsUser)
        {
            cart = await context.Carts.FirstOrDefaultAsync(x => x.UserId == cartIdentity.UserId, ct);
        }
        else
        {
            cart = await context.Carts.FirstOrDefaultAsync(x => x.GuestId == cartIdentity.GuestId, ct);
        }

        if (cart is null)
        {
            Result<Cart> createCartResult;
            if (cartIdentity.IsUser)
            {
                createCartResult = Cart.CreateForUser(cartIdGen.NewId(), cartIdentity.UserId!.Value);
            }
            else
            {
                createCartResult = Cart.CreateForGuest(cartIdGen.NewId(), cartIdentity.GuestId!.Value);

            }

            if (createCartResult.Failed)// this must not happen because there is no logic that can be broken here 
                return ApplicationErrors.Unexpected.UnableToAddThisItem;

            cart = createCartResult.Value;

            context.Carts.Add(cart);
        }

        var cartItemId = cartItemIdGen.NewId();

        var result = cart.AddItem(cartItemId, request.ParsedProductId, request.Quantity);

        if (result.Failed)
        {
            return result.Errors;
        }

        await context.SaveAsync(ct);

        return cartItemId.Value;
    }
}


