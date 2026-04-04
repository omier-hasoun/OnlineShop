namespace Application.Features.Products.Commands.Create;

public sealed record CreateProductCommand(
    string Title,
    string Description,
    string Brand
) : IRequest<Result<ProductId>>;
