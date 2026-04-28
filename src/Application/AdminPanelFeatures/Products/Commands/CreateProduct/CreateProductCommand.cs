
using Domain.Brands;
using Domain.Categories;

namespace Application.AdminPanelFeatures.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
BrandId BrandId, CategoryId CategoryId, string Title, string Description,
bool IsSerialized, IReadOnlyDictionary<string, string>? Attributes
) : IRequest<Result<long>>;
