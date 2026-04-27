
namespace Api.Controllers.Products.Requests;

public sealed record CreateProductRequest(
string BrandId, string CategoryId, string Title, string Description, bool IsSerialized, IReadOnlyDictionary<string, string> Attributes
);

