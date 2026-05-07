
namespace Application.AdminPanelFeatures.Products.Commands.PublishProduct;

public sealed record PublishProductCommand(long Product_Id, long? Variant_Id) : IRequest<Result<Success>>;

