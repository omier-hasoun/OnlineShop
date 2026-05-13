
namespace Application.Features.Management.ProductGroups.Commands.UnpublishProduct;

internal sealed class UnpublishProductCommandHandler : IRequestHandler<UnpublishProductCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(UnpublishProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
