
using Application;
using Application.Common.Configurations;
using Application.Common.Dtos;
using Infrastructure.Channels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Infrastructure.LocalServices.ImagesStore;

internal sealed class ImagesStoreService(IImageJobWriter imageJobWriter, IWebHostEnvironment webEnv, IFileStorageService fileStore, IOptions<ProductImagePathOptions> options) : IImageStorageService
{
    private readonly string _originalImagesDirPath = Path.Combine(webEnv.WebRootPath, options.Value.Images_Original);
    private readonly string _150x150ImagesDirPath = Path.Combine(webEnv.WebRootPath, options.Value.Images_200x200);
    private readonly string _500x375lImagesDirPath = Path.Combine(webEnv.WebRootPath, options.Value.Images_600x600);
    private readonly string _1600x1700ImagesDirPath = Path.Combine(webEnv.WebRootPath, options.Value.Images_2000x2000);


    public Result<Success> DeleteAll(List<string> fileNames)
    {
        List<string> filePaths = new(fileNames.Count * 3);//( * 3) because each file has 3 copies
        fileNames.ForEach(fileName =>
        {
            filePaths.Add(Path.Combine(_150x150ImagesDirPath, fileName));
            filePaths.Add(Path.Combine(_500x375lImagesDirPath, fileName));
            filePaths.Add(Path.Combine(_1600x1700ImagesDirPath, fileName));

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
