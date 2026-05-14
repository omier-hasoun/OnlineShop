
using Api.Requests;
using Application.Features.Public.Carts.Commands.AddCartItem;
using Application.Features.Public.Carts.Queries.GetCart;
using MediatR;

namespace Api.Controllers.Carts;

[Route("api/cart")]
public sealed class CartsController(IMediator mediator, ICartIdentityService cartIdentity) : ApiController
{
    [HttpGet()]
    public async Task<IActionResult> GetCartByUserIdOrGuestId(CancellationToken ct)
    {
        var result = await mediator.Send(new GetCartQuery(cartIdentity.GetCurrentIdentity()), ct);

        return result.Match((response) => Ok(response), Problem );
    }

    [HttpPost()]
    public async Task<IActionResult> AddItem([FromBody] AddCartItemRequest request, CancellationToken ct)
    {
        var result = await mediator.Send(new AddCartItemCommand(cartIdentity.GetCurrentIdentity(), request.ProductId, request.Quantity), ct);

        return result.Match((response) => Ok(new { cartItemId = response }), Problem);
    }
}
