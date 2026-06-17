
using Application.Common.Dtos;
using Application.Features.Public.Checkout.Dtos;

namespace Application.Features.Public.Checkout.Queries.ReviewOrderDetails;

public sealed record ReviewOrderDetailsQuery(UserIdentity Identity) : IRequest<Result<OrderPreviewDto>>
{
    
}
