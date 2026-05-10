namespace Application.Features.Management.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(
    long ProductId
) : IRequest<Result<Deleted>>;
