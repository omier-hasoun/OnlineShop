using Application.Features.Public.Checkout.Commands.PaymentSucceeded;
using Application.Features.Public.Checkout.Commands.ProceedToPayment;
using Application.Features.Public.Checkout.Queries.ReviewOrderDetails;

namespace Api.Controllers.Checkout;

[Route("api/checkout")]
public sealed class CheckoutController(IMediator mediator, ICurrentUserService currentUser) : ApiController
{
    [HttpGet("review-order")]
    public async Task<IActionResult> ReviewOrderDetails(CancellationToken ct)
    {
        var response = await mediator.Send(new ReviewOrderDetailsQuery(currentUser.GetCurrentIdentity()), ct);

        return response.Match((response) =>  Ok(response), Problem);
    }

    [HttpPost]
    public async Task<IActionResult> ProceedToPayment(CancellationToken ct)
    {
        var response = await mediator.Send(new ProceedToPaymentCommand(currentUser.GetCurrentIdentity()), ct);

        return response.Match((response) => Ok(response), Problem);
    }

    [HttpPost("payment/succeeded/{UserId}")]
    public async Task<IActionResult> PaymentSucceeded(string UserId, CancellationToken ct)
    {
        await mediator.Send(new PaymentSucceededCommand(UserId), ct);
        return Ok();
    }


    [HttpPost("payment/failed")]
    public async Task<IActionResult> PaymentFailed(CancellationToken ct)
    {
        var response = await mediator.Send(new ProceedToPaymentCommand(currentUser.GetCurrentIdentity()), ct);

        return response.Match((response) => Ok(response), Problem);
    }
}
