
namespace Application.Features.Public.Orders.Commands.RefundOrder;

public sealed record RefundOrderCommand(List<OrderId> OrderIds) : IRequest
{

}
