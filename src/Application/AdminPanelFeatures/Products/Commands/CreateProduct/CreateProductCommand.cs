using Application.Common.RequestModels;

namespace Application.AdminPanelFeatures.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
string BrandId, string CategoryId, string Title, string Description,
bool IsSerialized, IReadOnlyDictionary<string, string> Attributes
) : IRequest<Result<long>>;
