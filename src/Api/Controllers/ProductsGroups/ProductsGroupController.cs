
using Application.Features.Public.Products.Queries.GetProductById;
using Application.Features.Public.Products.Queries.ListProducts;
using MediatR;

namespace Api.Controllers.ProductsGroups;

[Route("api/products-group")]
public sealed class ProductsGroupController(IMediator mediator) : ApiController
{


    [HttpGet()]
    public async Task<IActionResult> ListProducts([FromQuery] ListProductsQuery request, CancellationToken ct)
    {
 
        var result = await mediator.Send(request, ct);

        return result.Match((response) => Ok(response), Problem);
    }

    [HttpGet("{id:required}")]
    public async Task<IActionResult> GetProductById([FromRoute] long id, CancellationToken ct)
    {

        var result = await mediator.Send(new GetProductByIdQuery(id), ct);

        return result.Match((response) => Ok(response), Problem);
    }


}
