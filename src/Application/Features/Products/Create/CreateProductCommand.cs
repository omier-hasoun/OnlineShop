namespace Application.Features.Products.Create;

public sealed record CreateProductCommand(
    string Name,
    string Description,
    string Manufacturer,
    int Quantity,
    decimal DefaultPrice
) : IRequest<Result<ProductId>>;
