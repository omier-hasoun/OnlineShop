
namespace Application.AdminPanelFeatures.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
Guid Brand_Id, long Category_Id, string Title, string Description, bool Is_Serialized, Dictionary<string, string> Attributes
) : IRequest<Result<long>>;
