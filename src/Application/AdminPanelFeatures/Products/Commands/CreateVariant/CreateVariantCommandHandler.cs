
using Application.Common.AppSettingsConfiguration.FileStoragePaths.ProductsPaths;
using Application.Common.Exceptions;
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
        // validate images
        var imagesValidationResult = AreValidImages(command.Images);

        if (imagesValidationResult.Failed)
        {
            return imagesValidationResult.Errors;
        }

        var product = await context.Products.Include(x => x.Variants).FirstOrDefaultAsync(x => x.Id == command.ProductId, ct);

        if (product is null)
        {
            return ApplicationErrors.NotFound.Product;
        }

        List<ProductImage> productImages = new(command.Images.Count);
        List<ImageProcessingTask> processImagesTasks = new (command.Images.Count);

        foreach(var image in command.Images)
        {

            if (FileHelper.TryGetExtesnionFromMediaType(image.File.ContentType, out string ext) is false)
            {
                // need to look what we gonna do later
                return ApplicationErrors.Validation.InvalidImage.WithParameters(image.File.FileName);
            }

            var fileName = nameGen.Generate();
            var fileNameWithExtension = fileName + ext;
            if (await fileStore.SaveImageAsync(image.File, fileNameWithExtension, ct))
            {
                productImages.Add(ProductImage.From(fileName, image.SortOrder).Value);

                processImagesTasks.Add(new ImageProcessingTask(fileNameWithExtension));
                continue;
            }
            
        }

        var variandId = idGen.NewId();

        var createVariantResult = product.AddVariant(variandId, Money.From(command.Price).Value,
            command.Width, command.Height, command.Length, command.Weight,
            command.Sku, command.Slug, command.BarCode, command.Specifications, productImages);

        if (createVariantResult.Failed)
        {
            return createVariantResult.Errors;
        }

        if (await context.SaveAsync(ct))
        {

            await imageProcessor.Process(processImagesTasks, ct);

            return variandId.Value;
        }

        throw new DbSaveFailedException();
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
