
using Application.Common.InternalModels;

namespace Infrastructure.Common.Abstractions;

internal interface IImageTaskReader
{
    IAsyncEnumerable<ImageProcessingTask> ReadAllAsync(CancellationToken ct);
}
