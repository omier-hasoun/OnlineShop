
namespace Infrastructure.LocalServices.FileStorageService;

internal sealed class LocalFileStorageService : IFileStorageService
{

    public void DeleteFile(string filePath)
    {
        if(Path.Exists(filePath))
            File.Delete(filePath);
    }

    public void DeleteAllFiles(List<string> filesPaths)
    {
        foreach (var file in filesPaths)
        {
            DeleteFile(file);
        }
    }

    public async Task<bool> SaveAsync(Stream stream, string filePath, CancellationToken ct)
    {
        if (stream == null || stream.Length == 0)
            return false;

        try
        {

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

            using var fileStream = new FileStream(filePath, FileMode.Create);

            await stream.CopyToAsync(fileStream, ct);

            await stream.FlushAsync(ct);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}
