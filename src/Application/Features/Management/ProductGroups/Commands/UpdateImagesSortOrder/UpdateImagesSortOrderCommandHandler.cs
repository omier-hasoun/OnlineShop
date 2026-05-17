
using Application.Common.Dtos;

namespace Application.Features.Management.ProductGroups.Commands.UpdateImagesSortOrder;

internal sealed class UpdateImagesSortOrderCommandHandler(IAppDbContext context) : IRequestHandler<UpdateImagesSortOrderCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateImagesSortOrderCommand request, CancellationToken ct)
    {

        var productGroup = await context.ProductGroups.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == request.ParsedProductGroupId, ct);

        if (productGroup is null)
            return ApplicationErrors.NotFound.Product;

        var result = productGroup.UpdateProductImagesSortOrder(request.ParsedProductId, ProductImageDto.ToProductImages(request.Images));

        if (result.Failed)
            return result.Errors;

        await context.SaveAsync(ct);

        return Result.Updated;
    }
}
