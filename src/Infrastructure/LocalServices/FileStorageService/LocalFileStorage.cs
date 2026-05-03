
using Microsoft.AspNetCore.Http;

namespace Infrastructure.LocalServices.FileStorageService;

internal sealed class LocalFileStorage : IFileStorageService
{
    public LocalFileStorage()
    {
    }

    public async Task<bool> SaveAsync(IFormFile file, string outputFilePath)
    {
        if (file == null || file.Length == 0)
            return false;

        try
        {
            var dir = Path.GetDirectoryName(outputFilePath);

            if (string.IsNullOrWhiteSpace(dir))
                return false;

            Directory.CreateDirectory(dir);

            await using var fileStream = new FileStream(
                outputFilePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                81920,
                useAsync: true);

            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();

            return true;
        }
        catch (Exception)
        {
            // log here
            return false;
        }
    }
}
