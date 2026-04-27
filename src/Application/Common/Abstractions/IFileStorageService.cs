

namespace Application.Common.Abstractions;

public interface IFileStorageService
{
    Task SaveAsync(IReadOnlyCollection<FileInfo> files);
}
