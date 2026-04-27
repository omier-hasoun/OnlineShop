using Api.Controllers.Products.Requests;
using Application.Common.RequestModels;
using Application.Features.Products.Commands.CreateProduct;
using MediatR;

namespace Api.Controllers.Products;


[Route("products/")]
public sealed class ProductsController(IMediator mediator) : ApiController
{

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {

        CreateProductCommand command = new
        (
            request.BrandId,
            request.CategoryId,
            request.Title,
            request.Description,
            request.IsSerialized,
            request.Attributes
        );

        var result = await mediator.Send(command, CancellationToken.None);

        if (result.Failed)
        {
            return BadRequest(result.Errors);
        }

        var ProductId = result.Value.Value;

        return Ok(ProductId);
    }
}
