
using Api.Requests;
using Application.Features.Public.Carts.Commands.AddCartItem;
using Application.Features.Public.Carts.Commands.RemoveCartItem;
using Application.Features.Public.Carts.Commands.UpdateCartItem;
using Application.Features.Public.Carts.Queries.GetCart;
using MediatR;

namespace Api.Controllers.Carts;

[Route("api/my-cart")]
public sealed class CartsController(IMediator mediator, ICartIdentityService cartIdentity) : ApiController
{
    [HttpGet()]
    public async Task<IActionResult> GetCartByUserIdOrGuestId(CancellationToken ct)
    {
        var result = await mediator.Send(new GetCartQuery(cartIdentity.GetCurrentIdentity()), ct);

        return result.Match((response) => Ok(response), Problem);
    }

    [HttpPost()]
    public async Task<IActionResult> AddItem([FromBody] AddCartItemRequest request, CancellationToken ct)
    {
        var result = await mediator.Send(new AddCartItemCommand(cartIdentity.GetCurrentIdentity(), request.ProductId, request.Quantity), ct);

        return result.Match((response) => Ok(new { cartItemId = response }), Problem);
    }

    [HttpDelete("{cartItemId:required}")]
    public async Task<IActionResult> RemoveItem(long cartItemId, CancellationToken ct)
    {
        var result = await mediator.Send(new RemoveCartItemCommand(cartItemId, cartIdentity.GetCurrentIdentity()), ct);

        return result.Match((response) => NoContent(), Problem);
    }

    [HttpPut("{cartItemId:required}")]
    public async Task<IActionResult> UpdateItem(long cartItemId, [FromBody] UpdateCartItemRequest request, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateCartItemCommand(cartIdentity.GetCurrentIdentity(), cartItemId, request.Quantity), ct);

        return result.Match((response) => NoContent(), Problem);
    }
}
