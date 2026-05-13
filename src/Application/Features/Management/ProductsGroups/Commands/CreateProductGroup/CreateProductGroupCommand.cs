namespace Application.Features.Management.ProductsGroups.Commands.CreateProductGroup;

public sealed record CreateProductGroupCommand(
Guid BrandId, long CategoryId, string Title, string Description, bool IsSerialized, Dictionary<string, string> Attributes
) : IRequest<Result<long>>;
