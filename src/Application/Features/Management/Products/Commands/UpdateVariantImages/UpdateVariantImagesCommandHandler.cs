using Application.Common.InternalModels;
using Application.Common.RequestModels;
using Domain.Products.ProductVariants;
using Domain.Products.ValueObjects;
using Shared.Helpers;

namespace Application.Features.Management.Products.Commands.UpdateVariantImages;

internal sealed class UpdateVariantImagesCommandHandler
(IAppDbContext context, IFileStorageService fileStore, IUniqueFileNameGenerator nameGen, IImageProcessingService imageProcessor, IFileSignetureValidator fileValidator)
: IRequestHandler<UpdateVariantImagesCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateVariantImagesCommand command, CancellationToken ct)
    {
        var areValid = AreValidImages(command.Images, out string? invalidFileName);

        if (!areValid)
            return ApplicationErrors.Validation.InvalidImage.WithParameters(invalidFileName!);


        ProductId productId = new(command.ProductId);
        ProductVariantId variantId = new(command.VariantId);

        List<ProductImage> productImages = new (command.Images.Count);
        List<ImageProcessingTask> processImagesTasks = new (command.Images.Count);

        var product = await context.Products.Include(x => x.Variants).FirstOrDefaultAsync(product => product.Id == productId, ct);

        if (product is null)
            return ApplicationErrors.NotFound.Product;

        foreach (var image in command.Images)
        {

            if (FileHelper.TryGetExtesnionFromMediaType(image.File.ContentType, out string ext) is false)
            {
                // this should not fail cause of the previous validation but just in case 
                return ApplicationErrors.Validation.InvalidImage.WithParameters(image.File.FileName);
            }

            var fileName = nameGen.Generate();
            var fileNameWithExtension = fileName + ext;

            if (await fileStore.SaveImageAsync(image.File, fileNameWithExtension, ct) is false)
            {
                return ApplicationErrors.InternalError.SavingImageFileFailed;
            }

            productImages.Add(ProductImage.From(fileName + ".webp", image.SortOrder).Value);// saving as .webp because the image will be converted to webp

            processImagesTasks.Add(new ImageProcessingTask(fileNameWithExtension));

        }

        var result = product.UpdateVariantImages(variantId, productImages);

        if (result.Failed)
            return result.Errors;

        await context.SaveAsync(ct);

        await imageProcessor.Process(processImagesTasks);

        return Result.Updated;
    }

    private bool AreValidImages(List<ProductImageUpload> Images, out string? fileName)
    {
        fileName = null;

        if ( Images is null || Images.Count ==  0)
            return false;

        foreach (var image in Images)
        {

            if (!fileValidator.Validate(image.File))
            {
                fileName = image.File.FileName!;
                return false;
            }
        }
        return true;
    }
}
