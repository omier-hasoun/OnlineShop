
using Api.Requests;
using Application.Features.Management.ProductGroups.Commands.CreateProductGroup;
using Application.Features.Management.ProductGroups.Commands.AddProduct;
using Application.Features.Management.ProductGroups.Commands.UpdateProductImages;
using MediatR;
using Application.Features.Management.ProductGroups.Commands.UpdateProductGroup;
using Application.Features.Management.ProductGroups.Queries.ListProducts;
using Application.Features.Management.ProductGroups.Queries.GetProductsGroupById;
using Application.Features.Management.ProductGroups.Commands.PublishProduct;
using Application.Features.Management.ProductGroups.Commands.UnpublishProduct;
using Application.Features.Management.ProductGroups.Commands.ArchiveProductGroup;
using Application.Features.Management.ProductGroups.Queries.GetProductById;
using Application.Common.RequestModels;

namespace Api.Controllers.Products;

[Route("api/management/")]
public sealed class ProductsManagementController(IMediator mediator) : ApiController
{
    #region commands
    [HttpPost("product-group")]
    public async Task<IActionResult> CreateProductGroup([FromBody] CreateProductGroupCommand request, CancellationToken ct)
    {

        var result = await mediator.Send(request, ct);

        return result.Match(response => Created(
                Url.Action(
                    nameof(GetProductGroupById),
                    "ProductsManagement",
                    new { Id = response }),

                    new { Id = response }),

                    Problem
        );
    }

    [HttpPut("product-group/{productGroupId:required}")]
    public async Task<IActionResult> UpdateProductGroup(long productGroupId, [FromBody] UpdateProductGroupRequest request, CancellationToken ct)
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
    [HttpPost("product-group/{productGroupId:long}/products")]
    public async Task<IActionResult> CreateProduct(long productGroupId, [FromBody] CreateProductCommand request, CancellationToken ct)
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

        return result.Match((response) => Created(Url.Action($"api/management/products/{productGroupId}/products/", new { Id = response }), response), Problem);
    }


    [HttpPost("product-group/{productGroupId:required}/publish")]
    public async Task<IActionResult> PublishProductGroup(long productGroupId, CancellationToken ct)
    {

        var result = await mediator.Send(new PublishProductGroupCommand(productGroupId), ct);

        return result.Match((response) => NoContent(), Problem);
    }
    [HttpPost("product-group/{productGroupId:required}/unpublish")]
    public async Task<IActionResult> UnpublishProductGroup(long productGroupId, CancellationToken ct)
    {

        var result = await mediator.Send(new UnpublishProductGroupCommand(productGroupId), ct);

        return result.Match((response) => NoContent(), Problem);
    }

    [HttpPost("product-group/{productGroupId:required}/archive")]
    public async Task<IActionResult> ArchiveProductGroup(long productGroupId, CancellationToken ct)
    {

        var result = await mediator.Send(new ArchiveProductGroupCommand(productGroupId), ct);

        return result.Match((response) => NoContent(), Problem);
    }


    [HttpPost("product-group/{productGroupId:required}/products/{productId:required}/publish")]
    public async Task<IActionResult> PublishProduct(long productGroupId, long productId, CancellationToken ct)
    {
        var result = await mediator.Send(new PublishProductCommand(productGroupId, productId), ct);

        return result.Match((response) => NoContent(), Problem);
    }

    [HttpPost("product-group/{productGroupId:required}/products/{productId:required}/unpublish")]
    public async Task<IActionResult> UnpublishProduct(long productGroupId, long productId, CancellationToken ct)
    {
        var result = await mediator.Send(new UnpublishProductCommand(productGroupId, productId), ct);

        return result.Match((response) => NoContent(), Problem);
    }

    [HttpPut("product-group/{productGroupId:required}/products/{productId:required}/images")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateVariantImages(long productGroupId, long productId, [FromForm] UpdateProductImagesRequest request, CancellationToken ct)
    {
        List<ProductImageUploadDto> imagesDto = new(request.Images.Count);

        request.Images.ForEach(image => imagesDto.Add(new ProductImageUploadDto
        {
            SortOrder = image.SortOrder,
            File = new FileUploadDto
            {
                ContentStream = image.File.OpenReadStream(),
                ContentLength = image.File.Length,
                MediaType = image.File.ContentType,
                FileName = image.File.FileName,
            }


        }
        ));

        var command = new UpdateProductImagesCommand
        {
            Images = imagesDto,
            ProductId = productId,
            ProductGroupId = productGroupId
        };
        var result = await mediator.Send(command, ct);

        return result.Match((response) => NoContent(), Problem);

    }

    #endregion


    #region queries

    [HttpGet("product-group/")]
    public async Task<IActionResult> ListProducts([FromQuery] ListProductsQuery request, CancellationToken ct)
    {

        var result = await mediator.Send(request, ct);

        return result.Match((response) => Ok(new { Products = response }), Problem);
    }

    [HttpGet("product-group/{productGroupId}")]
    public async Task<IActionResult> GetProductGroupById(long productGroupId, CancellationToken ct)
    {

        var result = await mediator.Send(new GetProductsGroupByIdQuery(productGroupId), ct);

        return result.Match((response) => Ok(new { response }), Problem);
    }

    [HttpGet("products/{productId}")]
    public async Task<IActionResult> GetProductById(long productId, CancellationToken ct)
    {

        var result = await mediator.Send(new GetProductByIdQuery(productId), ct);

        return result.Match((response) => Ok(new { response }), Problem);
    }

    #endregion
}


//[HttpPost("{productGroupId:required}/products/{productId:required}/archive")]
//public async Task<IActionResult> ArchiveVariant(long productGroupId, long productId, CancellationToken ct)
//{
//    var result = await mediator.Send(new , ct);

//    return result.Match((response) => NoContent(), Problem);
//}
