
namespace Application.Features.Management.Products.Commands.ChangeVariantState;
public sealed record ChangeVariantStateCommand(long ProductId, long VariantId, string Status) : IRequest<Result<Success>>;
