
using Application.Common.ResponseModels;
using Application.Features.Products.Dtos;
using Domain.Brands;
using Domain.Categories;
using Domain.Common.ValueObjects;

namespace Application.Features.Products.Queries.ListProducts;

public sealed record ListProductsQuery(int PageSize, int PageNumber, Money? MaxPrice, string? SearchText, BrandId? BrandId, CategoryId? CategoryId) : IRequest<Result<PaginatedList<ProductListItemDto>>>;
