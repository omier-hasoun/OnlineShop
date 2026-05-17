
using System.Threading.Channels;

namespace Infrastructure.Channels;

public sealed record ImageProcessingJob(string FileName);

internal sealed class ImageProcessingJobsChannel : IImageJobWriter, IImageJobReader
{
    private readonly Channel<ImageProcessingJob> _channel;

    public ImageProcessingJobsChannel(Channel<ImageProcessingJob> channel)
    {
        _channel = channel;
    }
    public IAsyncEnumerable<ImageProcessingJob> ReadAllAsync(CancellationToken ct)
    {
        return _channel.Reader.ReadAllAsync(ct);
    }

    public async ValueTask WriteAllAsync(List<ImageProcessingJob> processingImagesTasks)
    {
        processingImagesTasks.ForEach(async x => await _channel.Writer.WriteAsync(x, CancellationToken.None));
    }
}
