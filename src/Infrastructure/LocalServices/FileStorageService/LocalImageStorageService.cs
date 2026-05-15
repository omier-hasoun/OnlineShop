
using Application.Common.Configurations;
 
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.LocalServices.FileStorageService;

internal sealed class LocalImageStorageService : IImageStorageService
{
    private readonly IWebHostEnvironment _env;
    private readonly ProductImagePathOptions _paths;
    private readonly string _outputDir;
    public LocalImageStorageService(IWebHostEnvironment env, IOptions<ProductImagePathOptions> options)
    {
        _env = env;
        _paths = options.Value;
        _outputDir = Path.Combine(_env.WebRootPath, _paths.Images_Original);
    }

    public void DeleteImage(string fileNameWithExtension)
    {
        File.Delete(_outputDir + fileNameWithExtension);
    }

    public void DeleteAllImages(List<string> fileNamesWithExtesnions)
    {
        foreach (var file in fileNamesWithExtesnions)
        {
            DeleteImage(file);
        }
    }

    public async Task<bool> SaveImageAsync(Stream stream, string fileNameWithExtension, CancellationToken ct)
    {
        if (stream == null || stream.Length == 0)
            return false;

        var outputPath = Path.Combine(_outputDir + fileNameWithExtension);

        try
        {

            Directory.CreateDirectory(_outputDir);

            using var fileStream = new FileStream(outputPath, FileMode.Create);

            await stream.CopyToAsync(fileStream, ct);

            await stream.FlushAsync(ct);

            return true;
        }
        catch (Exception)
        {
            DeleteImage(fileNameWithExtension); // delete the file to not waste storage
            return false;
        }
    }

}
