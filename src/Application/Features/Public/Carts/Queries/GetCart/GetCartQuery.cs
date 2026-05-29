using Application.Common.Dtos;
using Application.Features.Public.Carts.Dtos;
using Domain.Common.ValueObjects;

namespace Application.Features.Public.Carts.Queries.GetCart;

public sealed record GetCartQuery(UserIdentity CartIdentity) : IRequest<Result<CartDto>>;
