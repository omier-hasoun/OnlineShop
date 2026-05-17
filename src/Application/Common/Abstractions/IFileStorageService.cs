
namespace Application.Common.Abstractions;

public interface IFileStorageService
{
    Task<bool> SaveAsync(Stream stream, string filePath, CancellationToken ct);
    void DeleteFile( string filePath);
    void DeleteAllFiles(List<string> filesPaths);


}
