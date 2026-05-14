
using Application.Features.Public.ProductsGroups.Queries.GetProductsGroupById;
using Application.Features.Public.ProductsGroups.Queries.ListProducts;
using MediatR;

namespace Api.Controllers.ProductsGroups;

[Route("api/products-groups")]
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

        var result = await mediator.Send(new GetProductsGroupByIdQuery(id), ct);

        return result.Match((response) => Ok(response), Problem);
    }


}
