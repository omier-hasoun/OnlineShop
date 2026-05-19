using Infrastructure.Channels;

namespace Infrastructure.Common.Abstractions;

internal interface IImageJobWriter
{
    ValueTask WriteAllAsync(List<ImageProcessingJob> jobs);

}
