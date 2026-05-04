
namespace Application.AdminPanelFeatures.Products.Commands.PublishProduct;

public sealed record UnpublishProductCommand(ProductId ProductId) : IRequest<Result<Success>>;

