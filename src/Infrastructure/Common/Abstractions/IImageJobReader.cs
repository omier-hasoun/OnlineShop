using Infrastructure.Channels;

namespace Infrastructure.Common.Abstractions;

internal interface IImageJobReader
{
    IAsyncEnumerable<ImageProcessingJob> ReadAllAsync(CancellationToken ct);
}
