

using Microsoft.AspNetCore.Http;

namespace Application.Common.Abstractions;

public interface IFileStorageService
{
    Task<bool> SaveImageAsync(IFormFile file, string outPutFilePath, CancellationToken ct);
}
