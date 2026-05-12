
using Application.Features.Public.Carts.Dtos;

namespace Application.Features.Public.Carts.Commands.GetItemsByGuestId;

internal sealed class GetItemsByGuestIdQueryHandler : IRequestHandler<GetItemsByGuestIdQuery, Result<CartDto>>
{
    public Task<Result<CartDto>> Handle(GetItemsByGuestIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
