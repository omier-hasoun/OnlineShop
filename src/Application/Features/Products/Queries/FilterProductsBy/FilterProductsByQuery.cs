using Application.Common.ResponseModels;
using Domain.Products.ProductVariants;

namespace Application.Features.Products.Queries.FilterProductsBy;

internal sealed record FilterProductsByQuery(
    string? Name,
    string? MadeByCompany,
    int? MinPrice,
    int? MaxPrice
) : IRequest<Result<PaginatedList<ProductVariant>>>;
