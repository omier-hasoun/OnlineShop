
using Application.Features.Management.Products.Commands.CreateProduct;
using Application.Features.Management.Products.Commands.CreateVariant;
using Application.Features.Management.Products.Commands.DeleteProduct;
using Application.Features.Management.Products.Commands.PublishProduct;
using Application.Features.Management.Products.Commands.UnpublishProduct;
using Application.Features.Management.Products.Commands.UpdateVariantImages;
using MediatR;

namespace Api.Controllers.Products;

[Route("api/management/products")]
public sealed class ProductsManagementController(IMediator mediator) : ApiController
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


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] long id, CancellationToken ct)
    {

        var result = await mediator.Send(new DeleteProductCommand(id), ct);

        return result.Match((response) => NoContent(), Problem);
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


    [HttpPost("variants/images")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateVariantImages([FromForm] UpdateVariantImagesCommand request, CancellationToken ct)
    {
        var result = await mediator.Send(request, ct);

        return result.Match((response) => NoContent(), Problem);

    }
}
