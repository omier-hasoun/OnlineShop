
using Application;
using Application.Common.Dtos;
using Infrastructure.Channels;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Storage.Images;

internal sealed class ImagesStorageService(IImageJobWriter imageJobWriter, IWebHostEnvironment webEnv, IFileStorageService fileStore, IOptions<MediaOptions> options) : IImageStorageService
{
    private readonly string _originalImagesDirPath = Path.Combine(webEnv.WebRootPath, options.Value.Images.Products.Original);
    private readonly string _smallImagesPath = Path.Combine(webEnv.WebRootPath, options.Value.Images.Products.Small);
    private readonly string _mediumImagesPath = Path.Combine(webEnv.WebRootPath, options.Value.Images.Products.Medium);
    private readonly string _largeImagesPath = Path.Combine(webEnv.WebRootPath, options.Value.Images.Products.Large);


    public Result<Success> DeleteAll(List<string> fileNames)
    {
        List<string> filePaths = new(fileNames.Count * 3); // each file has 3 copies
        fileNames.ForEach(fileName =>
        {
            filePaths.Add(Path.Combine(_smallImagesPath, fileName));
            filePaths.Add(Path.Combine(_mediumImagesPath, fileName));
            filePaths.Add(Path.Combine(_largeImagesPath, fileName));

        });

        fileStore.DeleteAllFiles(filePaths);
        return Result.Success;
    }

    public async ValueTask<Result<Success>> SaveAllAsync(IReadOnlyCollection<FileUploadDto> images, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(images);

        List<ImageProcessingJob> imageProcessJobs = new(images.Count);

        foreach (var image in images)
        {

            var filePath = Path.Combine(_originalImagesDirPath, image.InternalFileName);
            imageProcessJobs.Add(new ImageProcessingJob(filePath));

            if (await fileStore.SaveAsync(image.ContentStream, filePath, ct) is false)
            {
                if (imageProcessJobs.Count > 0)
                {
                    List<string> savedImages = new(imageProcessJobs.Count);

                    imageProcessJobs.ForEach(x => savedImages.Add(x.FileName));

                    fileStore.DeleteAllFiles(savedImages);
                }

                return ApplicationErrors.Unexpected.CouldntSaveImage;
            }

        }

        await imageJobWriter.WriteAllAsync(imageProcessJobs);

        return Result.Success;
    }
}
