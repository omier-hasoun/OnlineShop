
using System.Threading.Channels;
using Application.Common.AppSettingsConfiguration.FileStoragePaths.ProductsPaths;
using Domain.Products.ProductVariants;
using Domain.Products.ValueObjects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NetVips;
using static NetVips.Enums;

namespace Infrastructure.LocalServices.BackgroundServices;

internal sealed class ImagesProcessorWorker : BackgroundService
{
    public ImagesProcessorWorker(IOptions<ProductPathsOptions> options, IImageTaskReader reader, IWebHostEnvironment env)
    {
        _pathsOptions = options.Value;
        _reader = reader;
        _env = env;
    }

    private readonly ProductPathsOptions _pathsOptions;
    private readonly IImageTaskReader _reader;
    private readonly IWebHostEnvironment _env;

    public void CompressImageAndSave(string fileName)
    {

        var originalImageFilePath = Path.Combine(_env.WebRootPath, _pathsOptions.Images_Original, fileName);
        try
        {

            using var orignalImage = Image.NewFromFile(originalImageFilePath, memory: true, access: Access.Sequential, revalidate: false);

            using var image_1600x1700 = orignalImage.ThumbnailImage(width: 1600, height: 1700, Enums.Size.Down, noRotate: false, crop: Enums.Interesting.Centre,
                                         linear: false, intent: Enums.Intent.Perceptual, failOn: Enums.FailOn.Error);

            using var image_500x375 = image_1600x1700.ThumbnailImage(width: 500, height: 375, Enums.Size.Down, noRotate: false, crop: Enums.Interesting.Centre,
                                        linear: false, intent: Enums.Intent.Perceptual, failOn: Enums.FailOn.Error);

            using var image_150x150 = image_500x375.ThumbnailImage(width: 150, height: 150, Enums.Size.Down, noRotate: false, crop: Enums.Interesting.Centre,
                                        linear: false, intent: Enums.Intent.Perceptual, failOn: Enums.FailOn.Error);

            var ImagesDirectoryPath_1600x1700 = Path.Combine(
                _env.WebRootPath,
                _pathsOptions.Images_1600x1700);

            var ImagesDirectoryPath_500x375 = Path.Combine(
                _env.WebRootPath,
                _pathsOptions.Images_500x375);

            var ImagesDirectoryPath_150x150 = Path.Combine(
                _env.WebRootPath,
                _pathsOptions.Images_150x150);

            Directory.CreateDirectory(ImagesDirectoryPath_1600x1700);

            Directory.CreateDirectory(ImagesDirectoryPath_500x375);

            Directory.CreateDirectory(ImagesDirectoryPath_150x150);

            image_1600x1700.Webpsave(Path.Combine(ImagesDirectoryPath_1600x1700, Path.GetFileNameWithoutExtension(fileName) + ".webp"), q: 80, effort: 4, keep: Enums.ForeignKeep.None, preset: ForeignWebpPreset.Picture);
            image_500x375.Webpsave(Path.Combine(ImagesDirectoryPath_500x375, Path.GetFileNameWithoutExtension(fileName) + ".webp"), q: 80, effort: 6, keep: Enums.ForeignKeep.None, preset: ForeignWebpPreset.Picture);
            image_150x150.Webpsave(Path.Combine(ImagesDirectoryPath_150x150, Path.GetFileNameWithoutExtension(fileName) + ".webp"), q: 80, effort: 2, keep: Enums.ForeignKeep.None, preset: ForeignWebpPreset.Picture);

        }
        catch (VipsException)
        {

            throw;
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            File.Delete(originalImageFilePath);
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var task in _reader.ReadAllAsync(stoppingToken))
        {
            CompressImageAndSave(task.FileName);
        }
    }
}
