
namespace Application.Features.Management.ProductGroups.Commands.UnpublishProduct;

internal sealed class UnpublishProductGroupCommandHandler : IRequestHandler<UnpublishProductGroupCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(UnpublishProductGroupCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
