
using Application.Common.Dtos;

namespace Application.Common.Abstractions;

public interface IImageStorageService
{
    ValueTask<Result<Success>> SaveAllAsync(List<FileUploadDto> images, CancellationToken ct);

}
