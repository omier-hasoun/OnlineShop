using Infrastructure.Configurations.FileStorage;
using Microsoft.Extensions.Options;

namespace Infrastructure.LocalServices.FileStorageService;

internal sealed class LocalFileStorage : IFileStorageService
{
    private readonly FileStoragePathsOptions _options;

    public LocalFileStorage(IOptions<FileStoragePathsOptions> options)
    {
        _options = options.Value;
    }
    public async Task SaveAsync(IReadOnlyCollection<FileInfo> files)
    {
        foreach(var file in files)
        {
            
        }
    }

    //public async Task SaveAsync()
    //{

    //    using (StreamReader reader = new filePathStreamReader())
    //    {

    //    }
    //}

    //public string GetUniqueFileName(string extension)
    //{
    //    string uniqueFileName = $"{Guid.NewGuid().ToString("N")}.{extension}";
    //    return uniqueFileName;
    //}
}
