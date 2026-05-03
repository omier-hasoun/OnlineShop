

using Microsoft.AspNetCore.Http;

namespace Application.Common.Abstractions;

public interface IFileStorageService
{
    Task<bool> SaveAsync(IFormFile file, string outPutFilePath);
}
