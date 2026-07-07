
using Domain.Common.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.Features.Public.Checkout.Commands.PaymentSucceeded;

internal sealed class PaymentSucceededCommandHandler(IAppDbContext db, ILogger<PaymentSucceededCommandHandler> logger) : IRequestHandler<PaymentSucceededCommand>
{
    public async Task Handle(PaymentSucceededCommand request, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(request.UserId) || Guid.TryParse(request.UserId, out var userId))
        {
            logger.LogWarning("UserId was not provided");
            return;
        }

        var guestId = new GuestAccountId(userId);

        await db.Carts.Where(x => x.UserId == userId || x.GuestId == guestId)
                      .Select(x => x.Items)
                      .ExecuteDeleteAsync(ct);
    }
}
