using Application.Common.Dtos;
using Application.Features.Public.Carts.Dtos;
using Domain.Common.ValueObjects;

namespace Application.Features.Public.Carts.Queries.GetCart;

public sealed record GetCartQuery(CurrentUser CartIdentity) : IRequest<Result<CartDto>>;
