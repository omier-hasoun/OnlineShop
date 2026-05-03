using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using Application.Common.InternalModels;

namespace Infrastructure.Channels;

internal sealed class ProcessingImagesTasksChannel : IImageProcessingService, IImageTaskReader
{
    private readonly Channel<ImageProcessingTask> _channel;

    public ProcessingImagesTasksChannel(Channel<ImageProcessingTask> channel)
    {
        _channel = channel;
    }
    public IAsyncEnumerable<ImageProcessingTask> ReadAllAsync(CancellationToken ct)
    {
        return _channel.Reader.ReadAllAsync(ct);
    }

    public async ValueTask Process(List<ImageProcessingTask> processingImagesTasks, CancellationToken ct)
    {
        processingImagesTasks.ForEach(async x => await _channel.Writer.WriteAsync(x, ct));
    }

}
