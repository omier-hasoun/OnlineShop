
namespace Application.Common.Abstractions;

public interface IImageStorageService
{
    Task<bool> SaveImageAsync(Stream stream, string fileNameWithExtension, CancellationToken ct);
    void DeleteImage( string fileNameWithExtension);
    void DeleteAllImages(List<string> fileNameWithExtension);


}
