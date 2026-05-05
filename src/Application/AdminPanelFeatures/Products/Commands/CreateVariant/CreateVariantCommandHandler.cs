
using Application.Common.InternalModels;
using Application.Common.RequestModels;
using Domain.Common.ValueObjects;
using Domain.Products.ProductVariants;
using Domain.Products.ValueObjects;
using Shared.Helpers;

namespace Application.AdminPanelFeatures.Products.Commands.CreateVariant;

public sealed class CreateVariantCommandHandler( IAppDbContext context, IUniqueFileNameGenerator nameGen,
 IIdGenerator<ProductVariantId> idGen, IFileStorageService fileStore, IFileValidationService fileValidator, IImageProcessingService imageProcessor) : IRequestHandler<CreateVariantCommand, Result<long>>

{
    public async Task<Result<long>> Handle(CreateVariantCommand command, CancellationToken ct)
    {

        ProductId productId = new(command.Product_Id);

        Money price = Money.From(command.Price).Value;

        var product = await context.Products.Include(x => x.Variants).FirstOrDefaultAsync(x => x.Id == productId, ct);

        if (product is null)
        {
            return ApplicationErrors.NotFound.Product;
        }

        List<ProductImage> productImages = new(command.Images.Count);
        List<ImageProcessingTask> processImagesTasks = new (command.Images.Count);

        var imagesValidationResult = AreValidImages(command.Images);
        if (imagesValidationResult.Failed)
        {
            return imagesValidationResult.Errors;
        }

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

            productImages.Add(ProductImage.From(fileName + ".webp", image.SortOrder).Value);// i will save it as .webp because i know later it will be so

            processImagesTasks.Add(new ImageProcessingTask(fileNameWithExtension));

        }

        var variandId = idGen.NewId();

        var createVariantResult = product.AddVariant(variandId, price,
            command.Width, command.Height, command.Length, command.Weight,
            command.Sku, command.Slug, command.BarCode, command.Specifications, productImages);

        if (createVariantResult.Failed)
        {
            return createVariantResult.Errors;
        }

        await context.SaveAsync(ct);

        await imageProcessor.Process(processImagesTasks);

        return variandId.Value;
    }

    private Result<Success> AreValidImages(IReadOnlyCollection<ProductVariantImageUpload> Images)
    {
        foreach (var image in Images)
        {
            var result = fileValidator.Validate(image.File);
            if (result.Failed)
            {
                result.TopError.WithParameters(image.File.FileName);
            }
        }
        return Result.Success;
    }
}
