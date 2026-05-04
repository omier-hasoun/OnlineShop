using Api.Controllers.Products.Requests;
using Api.Controllers.Products.Responses;
using Application.AdminPanelFeatures.Products.Commands.CreateProduct;
using Application.AdminPanelFeatures.Products.Commands.CreateVariant;
using Application.AdminPanelFeatures.Products.Commands.PublishProduct;
using Application.Common.AppSettingsConfiguration.FileStoragePaths.ProductsPaths;
using Application.Common.Extensions;
using Application.Common.RequestModels;
using Application.Common.ResponseModels;
using Application.Features.Products.Queries.ListProducts;
using Domain.Brands;
using Domain.Categories;
using Domain.Products;
using MediatR;
using Microsoft.Extensions.Options;
using Shared.Results;

namespace Api.Controllers.Products;


[Route("products/")]
public sealed class ProductsController(IMediator mediator, IOptions<ProductPathsOptions> options) : ApiController
{

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken ct)
    {

        var brandId = new BrandId(request.Brand_Id);

        var categoryId = new CategoryId(request.Category_Id);

        CreateProductCommand command = new
        (
            brandId,
            categoryId,
            request.Title,
            request.Description,
            request.Is_Serialized,
            request.Attributes
        );

        var result = await mediator.Send(command, ct);

        return result.Match((response) => Ok(response), Problem);
    }

    [HttpPost("variants/")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateVariant([FromForm] CreateVariantRequest request, CancellationToken ct)
    {

        var productId = new ProductId(request.Product_Id);

        CreateVariantCommand command = new
        (
            productId,
            request.Price,
            request.Width,
            request.Height,
            request.Length,
            request.Weight,
            request.Sku,
            request.Slug,
            request.BarCode,
            request.Images,
            request.Specifications
        );

        var result = await mediator.Send(command, ct);

        return result.Match((response) => Ok(response), Problem);
    }

    [HttpGet()]
    public async Task<IActionResult> ListProducts([FromQuery] ListProductsQuery request, CancellationToken ct)
    {

        var queryResult = await mediator.Send(request, ct);

        List<ProductListItemResponse> response;
        Result<PaginatedList<ProductListItemResponse>> result = new();

        if(queryResult.Succeeded)
        {
            var dto = queryResult.Value;
            response = new(queryResult.Value.TotalCount);
            foreach(var item in queryResult.Value.Items!)
            {
                response.Add( new ProductListItemResponse()
                {
                    Id = item.Id.Value,
                    AverageRating = item.AverageRating.Value,
                    Brand = item.Brand,
                    ImageUrl = Url.Content($"{Request.Scheme}://{Request.Host}/{options.Value.Images_500x375}{item.Image.FileName}.webp"),
                    DiscountPercentage = item.DiscountPercentage,
                    PriceNow = (double)item.PriceNow.Value,
                    OriginalPrice = (double)item.OriginalPrice.Value,
                    Title = item.Title

                });
            }
            result = response.ToPaginatedList(queryResult.Value.PageNumber, queryResult.Value.TotalCount);
        }

        return result.Match((response) => Ok(response), Problem);
    }

    [HttpPatch("publish/{id}")]
    public async Task<IActionResult> PublishProduct([FromRoute] long id, CancellationToken ct)
    {
        var result = await mediator.Send(new PublishProductCommand(new(id)), ct);

        return result.Match((response) => NoContent(), Problem);
    }

    [HttpPatch("unpublish/{id}")]
    public async Task<IActionResult> UnpublishProduct([FromRoute] long id, CancellationToken ct)
    {
        var result = await mediator.Send(new UnpublishProductCommand(new(id)), ct);

        return result.Match((response) => NoContent(), Problem);
    }


    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetProductById([FromRoute] long id, CancellationToken ct)
    //{

    //    var result = await mediator.Send(id, ct);

    //    return result.Match((response) => Ok(response), Problem);
    //}

    //[HttpGet("variants/{id}")]
    //public async Task<IActionResult> GetVariantById([FromRoute]long id, CancellationToken ct)
    //{

    //    var result = await mediator.Send(id, ct);

    //    return result.Match((response) => Ok(response), Problem);
    //}
}
