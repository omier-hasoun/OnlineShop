using Application.Common.InternalModels;
using Application.Common.RequestModels;
using Domain.ProductsGroups.Products;
using Domain.ProductsGroups.ValueObjects;
using Shared.Helpers;

namespace Application.Features.Management.ProductGroups.Commands.UpdateProductImages;

internal sealed class UpdateProductImagesCommandHandler
(IAppDbContext context, IImageStorageService fileStore, IUniqueFileNameGenerator nameGen, IImageProcessingService imageProcessor, IImageValidator fileValidator)
: IRequestHandler<UpdateProductImagesCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateProductImagesCommand request, CancellationToken ct)
    {
        ProductsGroupId productGroupId = new(request.ProductGroupId);

        ProductId productId = new(request.ProductId);

        var product = await context.ProductGroups.Include(x => x.Products).FirstOrDefaultAsync(pg => pg.Id == productGroupId, ct);

        if (product is null)
            return ApplicationErrors.NotFound.Product;

        var Images = request.Images;

        List<ProductImage> productImages = new(Images.Count);
        List<ImageProcessingTask> processImagesTasks = new(Images.Count);
        List<string>? invalidImages = new(Images.Count);

        Images.ForEach(image =>
        {
            if (!IsValidImage(image.File))
            {
                invalidImages.Add(image.File.FileName);
            }
        });

        if (invalidImages.Count > 0)
            return ApplicationErrors.Validation.InvalidImage.WithParameters(invalidImages);

        foreach (var image in Images)
        {

            var fileName = nameGen.Generate();
            var fileNameWithExtension = fileName + FileHelper.GetExtensionFromMediaType(image.File.MediaType);

            if (await fileStore.SaveImageAsync(image.File.ContentStream, fileNameWithExtension, ct) is false)
            {
                if(processImagesTasks.Count > 0)
                {
                    List<string> fileNamesWithExt = new(processImagesTasks.Count);

                    processImagesTasks.ForEach(x => fileNamesWithExt.Add(x.FileName));

                    fileStore.DeleteAllImages(fileNamesWithExt);
                }

                return ApplicationErrors.Unexpected.SavingImageFileFailed;
            }

            productImages.Add(ProductImage.From(fileName + ".webp", image.SortOrder).Value);// saving as .webp because the image will be converted to webp

            processImagesTasks.Add(new ImageProcessingTask(fileNameWithExtension));
            

        }
        var result = product.UpdateProductImages(productId, productImages);

        if (result.Failed)
            return result.Errors;

        await context.SaveAsync(ct);

        await imageProcessor.StartProcessing(processImagesTasks);

        return Result.Updated;
    }

    private bool IsValidImage(FileUploadDto image)
    {

        if 
        (
            !ApplicationRules.Uploads.AllowedImageMediaTypesList.Contains(image.MediaType) || 
            !image.ContentStream.CanSeek ||
            !fileValidator.Validate(image.ContentStream, ApplicationRules.Uploads.MinWidth, ApplicationRules.Uploads.MinHeight)
        )
        {
            return false;
        }
        
        image.ContentStream.Position = 0L;
        return true;
    }
}
