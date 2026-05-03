using Api.Controllers.Products.Requests;
using Application.AdminPanelFeatures.Products.Commands.CreateProduct;
using Application.AdminPanelFeatures.Products.Commands.CreateVariant;
using Application.Common.RequestModels;
using Application.Features.Products.Queries.ListProducts;
using Domain.Brands;
using Domain.Categories;
using Domain.Products;
using MediatR;

namespace Api.Controllers.Products;


[Route("products/")]
public sealed class ProductsController(IMediator mediator) : ApiController
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

        var result = await mediator.Send(request, ct);

        return result.Match((response) => Ok(response), Problem);
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
