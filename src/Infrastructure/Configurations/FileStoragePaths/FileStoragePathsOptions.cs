using Infrastructure.Configurations.FileStoragePaths.ProductsPaths;

namespace Infrastructure.Configurations.FileStorage;

public sealed class FileStoragePathsOptions
{
    public ProductPathsOptions ProductsPaths { get; set; } = null!;
}
