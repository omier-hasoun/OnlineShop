using Api.Controllers.Products.Requests;
using Application.AdminPanelFeatures.Products.Commands.CreateProduct;
using Domain.Brands;
using Domain.Categories;
using MediatR;
using Shared.Results;

namespace Api.Controllers.Products;


[Route("products/")]
public sealed class ProductsController(IMediator mediator) : ApiController
{

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request, CancellationToken ct)
    {

        //var brandIdResult = BrandId.From(request.BrandId); 

        //if (brandIdResult.Failed)
        //{
        //    return Problem(brandIdResult.Errors);
        //}

        //var categoryIdResult = CategoryId.From(request.CategoryId);

        //if(categoryIdResult.Failed)
        //{
        //    return Problem(categoryIdResult.Errors);
        //}

        //CreateProductCommand command = new
        //(
        //    brandIdResult.Value,
        //    categoryIdResult.Value,
        //    request.Title,
        //    request.Description,
        //    request.IsSerialized,
        //    request.Attributes
        //);

        var result = await mediator.Send(request, ct);

        return result.Match(
            (response) => Ok(response),
           Problem);
    }
}
