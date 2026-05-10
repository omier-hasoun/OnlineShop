namespace Application.Features.Management.Products.Commands.UnpublishProduct;

public sealed record UnpublishProductCommand(long Product_Id, long? Variant_Id) : IRequest<Result<Success>>;

