
namespace Application.Features.Management.ProductGroups.Commands.PublishProduct;

internal sealed class PublishProductCommandHandler : IRequestHandler<PublishProductCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(PublishProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
