
using Application.AdminPanelFeatures.Products.Commands.CreateProduct;
using Application.AdminPanelFeatures.Products.Commands.CreateVariant;
using Application.AdminPanelFeatures.Products.Commands.PublishProduct;
using Application.Common.AppSettingsConfiguration.FileStoragePaths.ProductsPaths;
using Application.Common.Extensions;
using Application.Common.ResponseModels;
using Application.Features.Products.Queries.ListProducts;
using Domain.Brands;
using Domain.Categories;
using Domain.Common.ValueObjects;
using Domain.Products;
using MediatR;
using Microsoft.Extensions.Options;
using Shared.Results;

namespace Api.Controllers.Products;


[Route("products/")]
public sealed class ProductsController(IMediator mediator, IOptions<ProductPathsOptions> options) : ApiController
{

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request, CancellationToken ct)
    {

        var result = await mediator.Send(request, ct);

        return result.Match((response) => Ok(response), Problem);
    }

    [HttpPost("variants/")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateVariant([FromForm] CreateVariantCommand request, CancellationToken ct)
    {
        var result = await mediator.Send(request, ct);

        return result.Match((response) => Ok(response), Problem);
    }

    [HttpGet()]
    public async Task<IActionResult> ListProducts([FromQuery] ListProductsQuery request, CancellationToken ct)
    {
 
        var result = await mediator.Send(request, ct);

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
