
using Application.Features.Public.Carts.Dtos;

namespace Application.Features.Public.Carts.Commands.GetItemsByGuestId;

internal sealed class GetItemsByGuestIdQueryHandler(IAppDbContext context) : IRequestHandler<GetItemsByGuestIdQuery, Result<CartDto>>
{
    public Task<Result<CartDto>> Handle(GetItemsByGuestIdQuery query, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
