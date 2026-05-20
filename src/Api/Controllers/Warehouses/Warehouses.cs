
using Application.Features.Management.Warehouses.Commands.CreateWarehouse;

namespace Api.Controllers.Warehouses;

[Route("warehouses")]
public sealed class Warehouses(IMediator mediator) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateWarehouse(CreateWarehouseCommand request, CancellationToken ct)
    {

        var result = await mediator.Send(request, ct);

        return result.Match((response) => Created("", new { Id = response }), Problem);
    }
}
