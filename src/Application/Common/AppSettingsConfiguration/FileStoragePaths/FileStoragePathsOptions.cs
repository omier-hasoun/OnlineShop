using Application.Common.AppSettingsConfiguration.FileStoragePaths.ProductsPaths;



namespace Application.Common.AppSettingsConfiguration.FileStoragePaths;

public sealed class FileStoragePathsOptions
{
    public ProductPathsOptions ProductsPaths { get; set; } = null!;
}
