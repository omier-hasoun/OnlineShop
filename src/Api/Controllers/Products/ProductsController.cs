using Application.AdminPanelFeatures.Products.Commands.CreateProduct;
using Application.AdminPanelFeatures.Products.Commands.CreateVariant;
using Application.AdminPanelFeatures.Products.Commands.DeleteProduct;
using Application.AdminPanelFeatures.Products.Commands.PublishProduct;
using Application.AdminPanelFeatures.Products.Commands.UpdateVariantImages;
using Application.Features.Products.Queries.GetProductById;
using Application.Features.Products.Queries.ListProducts;
using IdGen;
using MediatR;

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
    public async Task<IActionResult> CreateVariant([FromBody] CreateVariantCommand request, CancellationToken ct)
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

    [HttpPatch("publish")]
    public async Task<IActionResult> PublishProduct([FromQuery] PublishProductCommand request, CancellationToken ct)
    {
        var result = await mediator.Send(request, ct);

        return result.Match((response) => NoContent(), Problem);
    }

    [HttpPatch("unpublish")]
    public async Task<IActionResult> UnpublishProduct([FromQuery] UnpublishProductCommand request, CancellationToken ct)
    {
        var result = await mediator.Send(request, ct);

        return result.Match((response) => NoContent(), Problem);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById([FromRoute] long id, CancellationToken ct)
    {

        var result = await mediator.Send(new GetProductByIdQuery(id), ct);

        return result.Match((response) => Ok(response), Problem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] long id, CancellationToken ct)
    {

        var result = await mediator.Send(new DeleteProductCommand(id), ct);

        return result.Match((response) => NoContent(), Problem);
    }

    [HttpPost("variants/images")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateVariantImages([FromForm] UpdateVariantImagesCommand request, CancellationToken ct)
    {
        var result = await mediator.Send(request, ct);

        return result.Match((response) => NoContent(), Problem);

    }
}
