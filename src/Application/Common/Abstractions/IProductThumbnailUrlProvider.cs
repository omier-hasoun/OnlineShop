
using Application.Common.Enums;

namespace Application.Common.Abstractions;

public interface IProductThumbnailUrlProvider
{
    string GetUrl(string imageName, ProductThumbnailSize size);

}

