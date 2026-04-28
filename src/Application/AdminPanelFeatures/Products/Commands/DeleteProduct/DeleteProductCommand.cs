namespace Application.AdminPanelFeatures.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(
    ProductId ProductId
) : IRequest<Result<Deleted>>;
