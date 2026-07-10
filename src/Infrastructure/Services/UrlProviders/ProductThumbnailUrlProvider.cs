
using Application.Common.Enums;
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.UrlProviders;

internal sealed class ProductThumbnailUrlProvider : IProductThumbnailUrlProvider
{
    private readonly MediaOptions _media;

    public ProductThumbnailUrlProvider(IOptions<MediaOptions> options)
    {
        _media = options.Value;
    }

    public string GetUrl(string imageName, ProductThumbnailSize size)
    {
        var path = size switch
        {
            ProductThumbnailSize.Small => _media.Images.Products.Small,
            ProductThumbnailSize.Medium => _media.Images.Products.Medium,
            ProductThumbnailSize.Large => _media.Images.Products.Large,
            _ => throw new ArgumentOutOfRangeException(nameof(size))
        };

        return $"{_media.BaseUrl}/{path}/{imageName}";
    }
}
