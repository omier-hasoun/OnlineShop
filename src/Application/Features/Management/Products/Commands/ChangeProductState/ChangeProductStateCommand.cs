namespace Application.Features.Management.Products.Commands.ChangeProductState;

public sealed record ChangeProductStateCommand(
    long ProductId,
    string Status
) : IRequest<Result<Updated>>;
