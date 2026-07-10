
namespace Application.Common.Abstractions;

public interface IImageStorageService
{
    ValueTask<Result<Success>> SaveAllAsync(IReadOnlyCollection<FileUploadDto> images, CancellationToken ct);
    Result<Success> DeleteAll(List<string> fileNames);


}
