

namespace Application.Common.Abstractions;

public interface IProductThumbnailUrlProvider
{
    string GetRelativeUrl(string imageName, ProductThumbnailSize size);
    string GetDefaultThumbnailUrl(ProductThumbnailSize size);
}
