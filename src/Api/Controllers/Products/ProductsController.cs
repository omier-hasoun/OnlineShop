using Application.AdminPanelFeatures.Products.Commands.CreateProduct;
using Application.AdminPanelFeatures.Products.Commands.CreateVariant;
using Application.AdminPanelFeatures.Products.Commands.PublishProduct;
using Application.Features.Products.Queries.GetProductById;
using Application.Features.Products.Queries.ListProducts;
using MediatR;
using Shared.Results;

namespace Api.Controllers.Products;


public sealed class ProductsController(IMediator mediator) : ApiController
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


    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById([FromRoute] long id, CancellationToken ct)
    {

        var result = await mediator.Send(new GetProductByIdQuery(id), ct);

        return result.Match((response) => Ok(response), Problem);
    }


}
