
using Application.Common.InternalModels;

namespace Application.Common.Abstractions;

public interface IImageProcessingService
{
    ValueTask StartProcessing(List<ImageProcessingTask> processingImagesTasks);

}
