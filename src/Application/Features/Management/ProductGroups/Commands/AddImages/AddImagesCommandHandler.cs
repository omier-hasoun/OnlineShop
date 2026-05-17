
using Application.Common.Dtos;
using Domain.ProductsGroups.Products;

namespace Application.Features.Management.ProductGroups.Commands.AddImages;

internal sealed class AddImagesCommandHandler(IAppDbContext context,  IImageValidator validator, IImageStorageService imageProcessor): IRequestHandler<AddImagesCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(AddImagesCommand request, CancellationToken ct)
    {
        ProductGroupId productGroupId = new(request.ProductGroupId);
        ProductId productId = new(request.ProductId);

        var productGroup = await context.ProductGroups.Include(x => x.Products).FirstOrDefaultAsync(pg => pg.Id == productGroupId, ct);

        if (productGroup is null || !productGroup.ProductExists(productId))
            return ApplicationErrors.NotFound.Product;

        validator.MinWidth = ApplicationRules.Uploads.MinWidth;
        validator.MinHeight = ApplicationRules.Uploads.MinHeight;
        validator.MaxSize = ApplicationRules.Uploads.MinHeight;

        var validationResult = validator.ValidateAll(request.Images);

        if (validationResult.Failed)
            return validationResult.Errors;

        List<FileUploadDto> imagesFiles = new(request.Images.Count);
        List<string> imageNames = new(request.Images.Count);

        request.Images.ForEach(image =>
        {
            imageNames.Add(image.InternalFileName);
        });

        var addProductResult = productGroup.AddProductImages(productId, imageNames);

        if (addProductResult.Failed)
            return addProductResult.Errors;

        var saveImagesResult = await imageProcessor.SaveAllAsync(request.Images, ct);

        if (saveImagesResult.Failed)
        {
            return saveImagesResult.Errors;
        }

        await context.SaveAsync(ct);

        return Result.Updated;
    }


}
