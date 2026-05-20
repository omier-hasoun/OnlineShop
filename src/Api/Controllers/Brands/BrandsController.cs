
using Application.Features.Public.Brands.Queries.ListBrands;

namespace Api.Controllers.Brands;

[Route("api/brands")]
public sealed class BrandsController(IMediator mediator) : ApiController
{


    [HttpGet]
    public async Task<IActionResult> ListBrands(CancellationToken ct)
    {

        var result = await mediator.Send(new ListBrandsQuery(), ct);

        return result.Match((response) => Ok(response), Problem);
    }
}
