using Application.Features.Public.Products.Dtos;
using Domain.Products.ProductVariants;

namespace Application.Features.Public.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(long ProductId) : IRequest<Result<ProductDto>>;
