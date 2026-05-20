
using Application.Features.Management.Warehouses.Commands.CreateWarehouse;
using Application.Features.Management.Warehouses.Commands.DeleteWarehouse;
using Application.Features.Management.Warehouses.Queries.GetWarehouseById;
using Application.Features.Management.Warehouses.Queries.ListWarehouses;
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

    [HttpGet]
    public async Task<IActionResult> ListWarehouse(CancellationToken ct)
    {

        var result = await mediator.Send(new ListWarehousesQuery(), ct);

        return result.Match((response) => Ok(response), Problem);
    }

    [HttpGet("{warehouseId}")]
    public async Task<IActionResult> ListWarehouse(long warehouseId, CancellationToken ct)
    {

        var result = await mediator.Send(new GetWarehouseByIdQuery(warehouseId), ct);

        return result.Match((response) => Ok(response), Problem);
    }
}
