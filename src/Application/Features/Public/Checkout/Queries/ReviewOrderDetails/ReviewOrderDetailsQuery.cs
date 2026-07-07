
using Application.Common.Dtos;
using Application.Features.Public.Checkout.Dtos;
using Domain.Services.Checkout;

namespace Application.Features.Public.Checkout.Queries.ReviewOrderDetails;

public sealed record ReviewOrderDetailsQuery(UserIdentity Identity) : IRequest<Result<OrderPreview>>
{
    
}
