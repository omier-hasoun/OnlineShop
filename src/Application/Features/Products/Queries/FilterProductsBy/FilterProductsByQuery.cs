namespace Application.Features.Products.Queries.FilterProductsBy;

public sealed record FilterProductsByQuery(
    string? Name,
    string? MadeByCompany,
    int? MinPrice,
    int? MaxPrice
) : IRequest<Result<Deleted>>;
