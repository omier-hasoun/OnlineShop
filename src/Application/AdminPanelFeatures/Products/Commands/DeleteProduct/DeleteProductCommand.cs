namespace Application.AdminPanelFeatures.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(
    long ProductId
) : IRequest<Result<Deleted>>;
