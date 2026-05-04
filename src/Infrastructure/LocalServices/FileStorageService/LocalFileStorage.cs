
using Application.Common.AppSettingsConfiguration.FileStoragePaths;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.LocalServices.FileStorageService;

internal sealed class LocalFileStorage : IFileStorageService
{
    private readonly IWebHostEnvironment _env;
    private readonly FileStoragePathsOptions _paths;
    public LocalFileStorage(IWebHostEnvironment env, IOptions<FileStoragePathsOptions> options)
    {
        _env = env;
        _paths = options.Value;
    }

    public async Task<bool> SaveImageAsync(IFormFile file, string fileName, CancellationToken ct)
    {
        if (file == null || file.Length == 0)
            return false;

        var outputPath = Path.Combine( _env.WebRootPath, _paths.ProductsPaths.Images_Original + fileName);
        try
        {

            Directory.CreateDirectory(outputPath);

            await using var fileStream = new FileStream(
                fileName,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                81920,
                useAsync: true);

            await file.CopyToAsync(fileStream, ct);
            await fileStream.FlushAsync(ct);

            return true;
        }
        catch (Exception)
        {
            File.Delete(outputPath); // case exception happed delete the file to not waste storage
            return false;
        }
    }
}
