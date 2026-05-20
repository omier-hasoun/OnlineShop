
using Application.Features.Management.Warehouses.Commands.CreateWarehouse;
using Application.Features.Management.Warehouses.Commands.DeleteWarehouse;
using Domain.Warehouses;

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

    [HttpDelete("{warehosueId}")]
    public async Task<IActionResult> DeleteWarehouse(long warehosueId, CancellationToken ct)
    {

        var result = await mediator.Send(new DeleteWarehouseCommand(warehosueId), ct);

        return result.Match((response) => NoContent(), Problem);
    }
}
