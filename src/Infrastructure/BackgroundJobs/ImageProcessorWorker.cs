
using Application.Common.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NetVips;
using static NetVips.Enums;

namespace Infrastructure.BackgroundJobs;

internal sealed class ImageProcessorWorker : BackgroundService
{
    public ImageProcessorWorker(IOptions<ProductImagePathOptions> options, IImageJobReader reader, IWebHostEnvironment env, IFileStorageService storageService)
    {
        _reader = reader;
        _storageService = storageService;

        _webRootPath = env.WebRootPath;

        _dir2000x2000 = Path.Combine(
            _webRootPath,
            options.Value.Images_2000x2000);

        _dir600x600 = Path.Combine(
        _webRootPath,
        options.Value.Images_600x600);

        _dir200x200 = Path.Combine(
            _webRootPath,
            options.Value.Images_200x200);


        _dirOriginal = Path.Combine(
            _webRootPath,
            options.Value.Images_Original);
    }

    private readonly IImageJobReader _reader;
    private readonly IFileStorageService _storageService;
    private readonly string _dir2000x2000;
    private readonly string _dir600x600;
    private readonly string _dir200x200;
    private readonly string _webRootPath;
    private readonly string _dirOriginal;




    public void CreateThumbnailsAndSave(string fileName)
    {
        string newFileName = Path.GetFileNameWithoutExtension(fileName) + ".webp";
        try
        {

            using var orignalImage = Image.NewFromFile(Path.Combine(_dirOriginal, newFileName), access: Access.Sequential);

            using var image1 = orignalImage.ThumbnailImage(width: 2000, height: 2000, Enums.Size.Down, noRotate: false, crop: Enums.Interesting.Centre,
                                         linear: false, intent: Enums.Intent.Perceptual, failOn: Enums.FailOn.Error);

            using var image2 = image1.ThumbnailImage(width: 600, height: 600, Enums.Size.Down, noRotate: false, crop: Enums.Interesting.Centre,
                                        linear: false, intent: Enums.Intent.Perceptual, failOn: Enums.FailOn.Error);

            using var image3 = image2.ThumbnailImage(width: 200, height: 200, Enums.Size.Down, noRotate: false, crop: Enums.Interesting.Centre,
                                        linear: false, intent: Enums.Intent.Perceptual, failOn: Enums.FailOn.Error);

            Directory.CreateDirectory(_dir2000x2000);

            Directory.CreateDirectory(_dir600x600);

            Directory.CreateDirectory(_dir200x200);

            image1.Webpsave(Path.Combine(_dir2000x2000, newFileName), q: 75, effort: 6, keep: Enums.ForeignKeep.None, preset: ForeignWebpPreset.Picture);
            image2.Webpsave(Path.Combine(_dir600x600, newFileName), q: 65, effort: 6, keep: Enums.ForeignKeep.None, preset: ForeignWebpPreset.Picture);
            image3.Webpsave(Path.Combine(_dir200x200, newFileName), q: 75, effort: 4, keep: Enums.ForeignKeep.None, preset: ForeignWebpPreset.Picture);

        }
        catch (Exception)
        {
            List<string> savedImages = [
                Path.Combine(_dir2000x2000, newFileName),
                Path.Combine(_dir600x600, newFileName),
                Path.Combine(_dir200x200, newFileName)]; // in case any error occured during saving

            _storageService.DeleteAllFiles(savedImages);
        }
        finally
        {
            _storageService.DeleteFile(Path.Combine(_dirOriginal, fileName)); // deleting the original file because its not needed anymore
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
