
using System.Threading.Channels;
using Application.Common.AppSettingsConfiguration.FileStoragePaths.ProductsPaths;
using Application.Common.Exceptions;
using Application.Common.InternalModels;
using Application.Common.RequestModels;
using Domain;
using Domain.Common.ValueObjects;
using Domain.Products.ProductVariants;
using Domain.Products.ValueObjects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Shared.Helpers;

namespace Application.AdminPanelFeatures.Products.Commands.CreateVariant;

public sealed class CreateVariantCommandHandler(IOptions<ProductPathsOptions> options, IAppDbContext context, IUniqueFileNameGenerator nameGen,
 IIdGenerator<ProductVariantId> idGen, IFileStorageService fileStore, IFileValidationService fileValidator, IImageProcessingService imageProcessor, IWebHostEnvironment env) : IRequestHandler<CreateVariantCommand, Result<long>>


{
    private readonly string _originalImagesPath= options.Value.Images_Original;
    public async Task<Result<long>> Handle(CreateVariantCommand command, CancellationToken ct)
    {
        // validate images
        var validImagesResult = AreValidImages(command.Images);
        if (validImagesResult.Failed)
        {
            return validImagesResult.Errors;
        }

        var product = await context.Products.FindAsync(command.ProductId);

        if (product is null)
        {
            return ApplicationErrors.NotFound.Product;
        }

        var variandId = idGen.NewId();

        var createVariantResult = product.AddVariant(variandId, Money.From(command.Price).Value,
            command.Width, command.Height, command.Length, command.Weight,
            command.Sku, command.Slug, command.BarCode, command.Specifications!);

        if (createVariantResult.Failed)
        {
            return createVariantResult.Errors;
        }

        int imagesCount = command.Images.Count;
        List<ProductImage> productImages = new(imagesCount);
        List<ImageProcessingTask> processingImagesTasks = new (imagesCount);

        for (int i = 0; i < imagesCount; i++)
        {

            if (FileHelper.TryGetExtesnionFromMediaType(command.Images[i].File.ContentType, out string ext) is false)
            {
                // need to look what we gonna do later
                return ApplicationErrors.Validation.InvalidImage.WithParameters(command.Images[i].File.FileName);
            }
            
            var fileName = nameGen.Generate() + ext;

            string outPutFilePath = Path.Combine(env.WebRootPath, _originalImagesPath, fileName);

            if (await fileStore.SaveAsync(command.Images[i].File, outPutFilePath))
            {
                productImages.Add(ProductImage.From(fileName, command.Images[i].SortOrder).Value);
                processingImagesTasks.Add(new ImageProcessingTask(fileName));
                continue;
            }
            
        }

        var updateResult = product.UpdateVariantImages(variandId, productImages);


        if (await context.SaveAsync(ct))
        {

            await imageProcessor.Process(processingImagesTasks, ct);

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
