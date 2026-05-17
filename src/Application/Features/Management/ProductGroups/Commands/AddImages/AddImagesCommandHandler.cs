
using Application.Common.Dtos;
using Domain.ProductsGroups.Products;

namespace Application.Features.Management.ProductGroups.Commands.AddImages;

internal sealed class AddImagesCommandHandler(IAppDbContext context,  IImageValidator validator, IImageStorageService store): IRequestHandler<AddImagesCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(AddImagesCommand request, CancellationToken ct)
    {
        ProductGroupId productGroupId =  request.ParsedProductGroupId;
        ProductId productId = request.ParsedProductId;

        var productGroup = await context.ProductGroups.Include(x => x.Products).FirstOrDefaultAsync(pg => pg.Id == productGroupId, ct);

        if (productGroup is null || !productGroup.ProductExists(productId))
            return ApplicationErrors.NotFound.Product;

        validator.MinWidth = ApplicationRules.Uploads.MinWidth;
        validator.MinHeight = ApplicationRules.Uploads.MinHeight;
        validator.MaxSize = ApplicationRules.Uploads.MaxProductImageSize;

        var validationResult = validator.ValidateAll(request.Images);

        if (validationResult.Failed)
            return validationResult.Errors;

        List<FileUploadDto> imagesFiles = new(request.Images.Count);
        List<string> imagesNames = new(request.Images.Count);

        foreach (var image in request.Images)
        {
            imagesNames.Add(image.InternalFileName);
        }


        var addProductResult = productGroup.AddProductImages(productId, imagesNames);

        if (addProductResult.Failed)
            return addProductResult.Errors;

        var saveImagesResult = await store.SaveAllAsync(request.Images, ct);

        if (saveImagesResult.Failed)
        {
            return saveImagesResult.Errors;
        }

        await context.SaveAsync(ct);

        return Result.Updated;
    }


}
