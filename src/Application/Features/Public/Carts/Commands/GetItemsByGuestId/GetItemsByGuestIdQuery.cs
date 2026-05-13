
using Application.Features.Public.Carts.Dtos;
using Domain.Common.ValueObjects;

namespace Application.Features.Public.Carts.Commands.GetItemsByGuestId;

public sealed record GetItemsByGuestIdQuery : IRequest<Result<CartDto>>
{
    public GuestAccountId GuestId { get; init; }

    public GetItemsByGuestIdQuery(Guid guestId)
    {
        GuestId = new GuestAccountId(guestId);
    }

}
