
using Application.Common.ResponseModels;
using Application.Features.Products.Dtos;

namespace Application.Features.Products.Queries.ListProducts;

public sealed record ListProductsQuery(int PageSize, int PageNumber) : IRequest<Result<PaginatedList<ProductListItemDto>>>;
