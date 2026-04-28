using Api.Controllers.Products.Requests;
using Application.AdminPanelFeatures.Products.Commands.CreateProduct;
using MediatR;

namespace Api.Controllers.Products;


[Route("products/")]
public sealed class ProductsController(IMediator mediator) : ApiController
{

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken ct)
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

        var result = await mediator.Send(command, ct);

        return result.Match(
            (response) => Ok(response),
           Problem);
    }
}
