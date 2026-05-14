
using Application.Features.Public.ProductsGroups.Queries.GetProductsGroupById;
using Application.Features.Public.ProductsGroups.Queries.ListProducts;
using MediatR;

namespace Api.Controllers.Products;

[Route("api/products-groups")]
public sealed class ProductsController(IMediator mediator) : ApiController
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

        var result = await mediator.Send(new GetProductsGroupByIdQuery(id), ct);

        return result.Match((response) => Ok(response), Problem);
    }


}
