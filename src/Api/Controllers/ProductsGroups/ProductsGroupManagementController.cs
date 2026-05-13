
using Api.Requests;
using Application.Features.Management.ProductsGroups.Commands.CreateProductGroup;
using Application.Features.Management.ProductsGroups.Commands.AddProduct;
using Application.Features.Management.ProductsGroups.Commands.UpdateProductImages;
using MediatR;
using Application.Features.Management.ProductsGroups.Commands.UpdateProductGroup;
using Application.Features.Management.ProductsGroups.Queries.ListProducts;
using Application.Features.Management.ProductsGroups.Queries.GetProductById;
using Application.Features.Management.ProductsGroups.Commands.PublishProduct;
using Application.Features.Management.ProductsGroups.Commands.UnpublishProduct;
using Application.Features.Management.ProductsGroups.Commands.ArchiveProductGroup;

namespace Api.Controllers.ProductsGroups;

[Route("api/management/products-group/")]
public sealed class ProductsGroupManagementController(IMediator mediator) : ApiController
{

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductGroupCommand request, CancellationToken ct)
    {

        var result = await mediator.Send(request, ct);

        return result.Match((response) => Created(Url.Action("api/management/products/", new { response }), new { response }), Problem);
    }

    [HttpPut("{productGroupId:required}")]
    public async Task<IActionResult> UpdateProduct(long productGroupId, [FromBody] UpdateProductRequest request, CancellationToken ct)
    {
        var command = new UpdateProductGroupCommand(
            productGroupId,
            request.New_Brand_Id,
            request.New_Category_Id,
            request.New_Description,
            request.New_Description,
            request.New_Is_Serialized,
            request.New_Attributes);

        var result = await mediator.Send(command, ct);

        return result.Match((response) => NoContent(), Problem);
    }


    [HttpPost("{productGroupId:long}/products")]
    public async Task<IActionResult> CreateVariant(long productGroupId, [FromBody] CreateVariantRequest request, CancellationToken ct)
    {
        var command = new AddProductCommand(
            productGroupId,
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

        return result.Match((response) => Created( Url.Action($"api/management/products/{productGroupId}/products/", new { response }), response), Problem);
    }


    [HttpPost("{productGroupId:required}/publish")]
    public async Task<IActionResult> PublishProductGroup(long productGroupId, CancellationToken ct)
    {

        var result = await mediator.Send(new PublishProductGroupCommand(productGroupId), ct);

        return result.Match((response) => NoContent(), Problem);
    }
    [HttpPost("{productGroupId:required}/unpublish")]
    public async Task<IActionResult> UnpublishProductGroup(long productGroupId, CancellationToken ct)
    {

        var result = await mediator.Send(new UnpublishProductGroupCommand(productGroupId), ct);

        return result.Match((response) => NoContent(), Problem);
    }

    [HttpPost("{productGroupId:required}/archive")]
    public async Task<IActionResult> ArchiveProductGroup(long productGroupId, CancellationToken ct)
    {

        var result = await mediator.Send(new ArchiveProductGroupCommand(productGroupId), ct);

        return result.Match((response) => NoContent(), Problem);
    }


    [HttpPost("{productGroupId:required}/products/{productId:required}/publish")]
    public async Task<IActionResult> PublishProduct(long productGroupId, long productId, CancellationToken ct)
    {
        var result = await mediator.Send(new PublishProductCommand(productGroupId, productId), ct);

        return result.Match((response) => NoContent(), Problem);
    }

    [HttpPost("{productGroupId:required}/products/{productId:required}/unpublish")]
    public async Task<IActionResult> UnpublishProduct(long productGroupId, long productId, CancellationToken ct)
    {
        var result = await mediator.Send(new UnpublishProductCommand(productGroupId, productId), ct);

        return result.Match((response) => NoContent(), Problem);
    }

    //[HttpPost("{productGroupId:required}/products/{productId:required}/archive")]
    //public async Task<IActionResult> ArchiveVariant(long productGroupId, long productId, CancellationToken ct)
    //{
    //    var result = await mediator.Send(new , ct);

    //    return result.Match((response) => NoContent(), Problem);
    //}

    [HttpPut("{productGroupId:required}/products/{productId:required}/images")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateVariantImages(long productGroupId, long productId, [FromForm] UpdateProductImagesRequest request, CancellationToken ct)
    {
        var command = new UpdateProductImagesCommand
        {
            Images = request.Images,
            VariantId = productId,
            ProductId = productGroupId
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

    [HttpGet("{productGroupId:required}")]
    public async Task<IActionResult> GetProductById(long productGroupId, CancellationToken ct)
    {

        var result = await mediator.Send(new GetProductByIdQuery(productGroupId), ct);

        return result.Match((response) => Ok(response), Problem);
    }
}
