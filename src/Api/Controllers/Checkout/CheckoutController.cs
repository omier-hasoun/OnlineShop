using Application.Features.Public.Checkout.Commands.BeginCheckout;
using Application.Features.Public.Checkout.Queries.ReviewOrderDetails;

namespace Api.Controllers.Checkout;

[Route("api/Checkout")]
public sealed class CheckoutController(IMediator mediator, ICurrentUserService currentUser) : ApiController
{
    [HttpGet("order-details")]
    public async Task<IActionResult> ReviewOrderDetails(CancellationToken ct)
    {
        var response = await mediator.Send(new ReviewOrderDetailsQuery(currentUser.GetCurrentIdentity()), ct);

        return response.Match((response) =>  Ok(response), Problem);
    }
}
