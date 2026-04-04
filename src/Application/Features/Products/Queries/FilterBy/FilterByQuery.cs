namespace Application.Features.Products.Queries.FilterBy;

public sealed record FilterByQuery(
    string? Name,
    string? MadeByCompany,
    int? MinPrice,
    int? MaxPrice
) : IRequest<Result<Deleted>>;
