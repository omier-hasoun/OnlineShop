
using Application.Common.Dtos;

namespace Application.Features.Public.Orders.Commands.PlaceOrder;

public sealed record PlaceOrderCommand() : IRequest<Result<Success>>
{
}
