
using Application.AdminPanelFeatures.Products.Commands.CreateProduct;

namespace Api.Controllers.Products.Requests;

public sealed record CreateProductRequest(
Guid BrandId, long CategoryId, string Title, string Description, bool IsSerialized, IReadOnlyDictionary<string, string>? Attributes
);

