
using Domain.Warehouses;

namespace Application.Features.Management.Warehouses.Dtos;

public sealed record WarehouseListItemDto
{
    public WarehouseListItemDto(WarehouseId id, string name)
    {
        this.Id = id.Value;
        this.Name = name;
    }

    public long Id { get; }
    public string Name { get; }
}
