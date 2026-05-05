
using Application.Features.Products.Dtos;
using Domain.Products.ProductVariants;

namespace Application.Features.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(long ProductId) : IRequest<Result<ProductDto>>;
