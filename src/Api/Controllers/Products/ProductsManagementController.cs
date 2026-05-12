
using Api.Requests;
using Application.Features.Management.Products.Commands.CreateProduct;
using Application.Features.Management.Products.Commands.CreateVariant;
using Application.Features.Management.Products.Commands.ChangeProductState;
using Application.Features.Management.Products.Commands.ChangeVariantState;
using Application.Features.Management.Products.Commands.UpdateVariantImages;
using MediatR;
using Application.Features.Management.Products.Commands.UpdateProduct;
using Application.Features.Management.Products.Queries.ListProducts;
using Application.Features.Management.Products.Queries.GetProductById;

namespace Api.Controllers.Products;

[Route("api/management/products/")]
public sealed class ProductsManagementController(IMediator mediator) : ApiController
{

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request, CancellationToken ct)
    {

        var result = await mediator.Send(request, ct);

        return result.Match((response) => Created(Url.Action("api/management/products/", new { response }), new { response }), Problem);
    }

    [HttpPut("{productId:required}")]
    public async Task<IActionResult> UpdateProduct(long productId, [FromBody] UpdateProductRequest request, CancellationToken ct)
    {
        var command = new UpdateProductCommand(
            productId,
            request.New_Brand_Id,
            request.New_Category_Id,
            request.New_Description,
            request.New_Description,
            request.New_Is_Serialized,
            request.New_Attributes);

        var result = await mediator.Send(command, ct);

        return result.Match((response) => NoContent(), Problem);
    }


    [HttpPost("{productId:long}/variants")]
    public async Task<IActionResult> CreateVariant(long productId, [FromBody] CreateVariantRequest request, CancellationToken ct)
    {
        var command = new CreateVariantCommand(
            productId,
            request.Price,
            request.Width,
            request.Height,
            request.Length,
            request.Weight,
            request.Sku,
            request.Slug,
            request.BarCode,
            request.Specifications);

        var result = await mediator.Send(command, ct);

        return result.Match((response) => Created( Url.Action($"api/management/products/{productId}/variants/", new { response }), response), Problem);
    }


    [HttpPatch("{productId:required}")]
    public async Task<IActionResult> ChangeProductState(long productId, [FromBody] ChangeProductStatusRequest request, CancellationToken ct)
    {

        var result = await mediator.Send(new ChangeProductStateCommand(productId, request.status), ct);

        return result.Match((response) => NoContent(), Problem);
    }

    [HttpPatch("{productId:required}/variants/{variantId:required}")]
    public async Task<IActionResult> ChangeVariantState(long productId, long variantId, [FromBody] ChangeVariantStatusRequest request, CancellationToken ct)
    {
        var result = await mediator.Send(new ChangeVariantStateCommand(productId, variantId, request.status), ct);

        return result.Match((response) => NoContent(), Problem);
    }


    [HttpPut("{productId:required}/variants/{variantId:required}/images")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateVariantImages(long productId, long variantId, [FromForm] UpdateProductImagesRequest request, CancellationToken ct)
    {
        var command = new UpdateVariantImagesCommand
        {
            Images = request.Images,
            VariantId = variantId,
            ProductId = productId
        };
        var result = await mediator.Send(command, ct);

        return result.Match((response) => NoContent(), Problem);

    }


    [HttpGet()]
    public async Task<IActionResult> ListProducts([FromQuery] ListProductsQuery request, CancellationToken ct)
    {

        var result = await mediator.Send(request, ct);

        return result.Match((response) => Ok(response), Problem);
    }

    [HttpGet("{productId:required}")]
    public async Task<IActionResult> GetProductById(long productId, CancellationToken ct)
    {

        var result = await mediator.Send(new GetProductByIdQuery(productId), ct);

        return result.Match((response) => Ok(response), Problem);
    }
}
