namespace Application.Features.Products.FilterBy;

public sealed record FilterByQuery(
    string? Name,
    string? MadeByCompany,
    int? MinPrice,
    int? MaxPrice
) : IRequest<Result<Deleted>>;
