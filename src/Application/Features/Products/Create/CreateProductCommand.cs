namespace Application.Features.Products.Create;

public sealed record CreateProductCommand(
    string Name,
    string Description,
    string MadeByCompany,
    int Quantity,
    decimal Price
) : IRequest<Result<ProductId>>;
