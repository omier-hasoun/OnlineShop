namespace Application.Features.Management.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
Guid BrandId, long CategoryId, string Title, string Description, bool IsSerialized, Dictionary<string, string> Attributes
) : IRequest<Result<long>>;
