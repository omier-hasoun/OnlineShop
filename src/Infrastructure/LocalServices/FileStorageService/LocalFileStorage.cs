
using Application.Common.Configurations;
 
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.LocalServices.FileStorageService;

internal sealed class LocalFileStorage : IFileStorageService
{
    private readonly IWebHostEnvironment _env;
    private readonly ProductImagePathOptions _paths;
    public LocalFileStorage(IWebHostEnvironment env, IOptions<ProductImagePathOptions> options)
    {
        _env = env;
        _paths = options.Value;
    }

    public async Task<bool> SaveImageAsync(IFormFile file, string fileName, CancellationToken ct)
    {
        if (file == null || file.Length == 0)
            return false;

        var outputPath = Path.Combine(_env.WebRootPath, _paths.Images_Original + fileName);
        try
        {

            Directory.CreateDirectory(Path.Combine(_env.WebRootPath, _paths.Images_Original));

            using var fileStream = new FileStream(outputPath, FileMode.Create);

            await file.CopyToAsync(fileStream, ct);

            return true;
        }
        catch (Exception)
        {
            File.Delete(outputPath); // case exception happed delete the file to not waste storage
            return false;
        }
    }
}
