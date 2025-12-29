namespace Application.Features.Products.Delete;

public sealed record DeleteProductCommand(
    ProductId ProductId
) : IRequest<Result<Deleted>>;
