
namespace Application.Features.Management.ProductGroups.Commands.PublishProduct;

internal sealed class PublishProductGroupCommandHandler : IRequestHandler<PublishProductGroupCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(PublishProductGroupCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
