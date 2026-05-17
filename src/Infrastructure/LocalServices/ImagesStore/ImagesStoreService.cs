
using Application;
using Application.Common.Configurations;
using Application.Common.Dtos;
using Infrastructure.Channels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Infrastructure.LocalServices.ImagesStore;

internal sealed class ImagesStoreService(IImageJobWriter imageJobWriter, IWebHostEnvironment webEnv, IFileStorageService fileStore, IOptions<ProductImagePathOptions> options) : IImageStorageService
{
    private readonly string _directoryPath = Path.Combine(webEnv.WebRootPath, options.Value.Images_Original);
    public async ValueTask<Result<Success>> SaveAllAsync(List<FileUploadDto> images, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(images);

        List<ImageProcessingJob> imageProcessJobs = new(images.Count);

        foreach (var image in images)
        {

            var filePath = Path.Combine(_directoryPath, image.InternalFileName);
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
