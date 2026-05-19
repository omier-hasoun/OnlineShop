
using Application.Common.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NetVips;
using static NetVips.Enums;

namespace Infrastructure.BackgroundServices;

internal sealed class ImageProcessorWorker : BackgroundService
{
    public ImageProcessorWorker(IOptions<ProductImagePathOptions> options, IImageJobReader reader, IWebHostEnvironment env, IFileStorageService storageService)
    {
        _pathsOptions = options.Value;
        _reader = reader;
        _webEnv = env;
        _storageService = storageService;

        _images_1600x1700_Directory = Path.Combine(
            _webEnv.WebRootPath,
            _pathsOptions.Images_1600x1700);

        _images_500x375_Director = Path.Combine(
        _webEnv.WebRootPath,
        _pathsOptions.Images_500x375);

        _images_150x150_Directory = Path.Combine(
            _webEnv.WebRootPath,
            _pathsOptions.Images_150x150);
    }

    private readonly ProductImagePathOptions _pathsOptions;
    private readonly IImageJobReader _reader;
    private readonly IWebHostEnvironment _webEnv;
    private readonly IFileStorageService _storageService;
    private readonly string _images_1600x1700_Directory;
    private readonly string _images_500x375_Director;
    private readonly string _images_150x150_Directory;


    public void CreateThumbnailsAndSave(string fileName)
    {

        string originalImageFilePath = Path.Combine(_webEnv.WebRootPath, _pathsOptions.Images_Original, fileName);
        string newFileName = Path.GetFileNameWithoutExtension(fileName) + ".webp";
        try
        {

            using var orignalImage = Image.NewFromFile(originalImageFilePath, access: Access.Sequential);

            using var image_1600x1700 = orignalImage.ThumbnailImage(width: 1600, height: 1700, Enums.Size.Down, noRotate: false, crop: Enums.Interesting.Centre,
                                         linear: false, intent: Enums.Intent.Perceptual, failOn: Enums.FailOn.Error);

            using var image_500x375 = image_1600x1700.ThumbnailImage(width: 500, height: 375, Enums.Size.Down, noRotate: false, crop: Enums.Interesting.Centre,
                                        linear: false, intent: Enums.Intent.Perceptual, failOn: Enums.FailOn.Error);

            using var image_150x150 = image_500x375.ThumbnailImage(width: 150, height: 150, Enums.Size.Down, noRotate: false, crop: Enums.Interesting.Centre,
                                        linear: false, intent: Enums.Intent.Perceptual, failOn: Enums.FailOn.Error);

            Directory.CreateDirectory(_images_1600x1700_Directory);

            Directory.CreateDirectory(_images_500x375_Director);

            Directory.CreateDirectory(_images_150x150_Directory);

            image_1600x1700.Webpsave(Path.Combine(_images_1600x1700_Directory, newFileName), q: 80, effort: 4, keep: Enums.ForeignKeep.None, preset: ForeignWebpPreset.Picture);
            image_500x375.Webpsave(Path.Combine(_images_500x375_Director, newFileName), q: 80, effort: 6, keep: Enums.ForeignKeep.None, preset: ForeignWebpPreset.Picture);
            image_150x150.Webpsave(Path.Combine(_images_150x150_Directory, newFileName), q: 80, effort: 1, keep: Enums.ForeignKeep.None, preset: ForeignWebpPreset.Picture);

        }
        catch (Exception)
        {
            List<string> savedImages = [
                Path.Combine(_images_1600x1700_Directory, newFileName),
                Path.Combine(_images_500x375_Director, newFileName),
                Path.Combine(_images_150x150_Directory, newFileName)]; // in case any error occured during saving

            _storageService.DeleteAllFiles(savedImages);
        }
        finally
        {
            _storageService.DeleteFile(originalImageFilePath); // deleting the original file because its not needed anymore
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var imageTask in _reader.ReadAllAsync(stoppingToken))
        {
            CreateThumbnailsAndSave(imageTask.FileName);
        }
    }
}
