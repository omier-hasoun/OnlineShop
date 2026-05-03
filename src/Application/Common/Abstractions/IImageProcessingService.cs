
using Application.Common.InternalModels;

namespace Application.Common.Abstractions;

public interface IImageProcessingService
{
    ValueTask Process(List<ImageProcessingTask> processingImagesTasks, CancellationToken ct);

}
