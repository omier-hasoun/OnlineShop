
namespace Application.AdminPanelFeatures.Products.Commands.PublishProduct;

public sealed record PublishProductCommand(ProductId ProductId) : IRequest<Result<Success>>;

