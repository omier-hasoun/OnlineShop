
using Application.Common.Dtos;
using Application.Features.Public.Checkout.Dtos;
using Domain.Services.Checkout;

namespace Application.Features.Public.Checkout.Queries.ReviewOrderDetails;

public sealed record ReviewOrderDetailsQuery(CurrentUser Identity) : IRequest<Result<OrderPreview>>
{
    
}
