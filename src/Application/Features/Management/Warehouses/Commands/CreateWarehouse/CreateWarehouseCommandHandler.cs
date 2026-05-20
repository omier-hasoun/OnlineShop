
using Domain.Common.Entities.Addresses;
using Domain.Warehouses;

namespace Application.Features.Management.Warehouses.Commands.CreateWarehouse;

internal sealed class CreateWarehouseCommandHandler(IAppDbContext context, IIdGenerator<WarehouseId> warehouseIdGen, IIdGenerator<AddressId> addressIdGen) 
: IRequestHandler<CreateWarehouseCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateWarehouseCommand request, CancellationToken ct)
    {
        var addressId = addressIdGen.NewId();
        var addressInfo = request.Address;
        var createAddressResult = Address.Create(addressId,
                                                 addressInfo.FullName,
                                                 addressInfo.PhoneNumber,
                                                 addressInfo.CountryCode,
                                                 addressInfo.HouseNo,
                                                 addressInfo.City,
                                                 addressInfo.PostalCode,
                                                 addressInfo.AddressLine1,
                                                 addressInfo.AddressLine2,
                                                 addressInfo.StateProvince,
                                                 null,
                                                 addressInfo.Notes);
        if (createAddressResult.Failed)
            return createAddressResult.Errors;


        var warehouseId = warehouseIdGen.NewId();

        var createWarehouseResult = Warehouse.Create(warehouseId, addressId, request.WarehouseName);

        if (createWarehouseResult.Failed)
            return createAddressResult.Errors;

        context.Addresses.Add(createAddressResult.Value);
        context.Warehouses.Add(createWarehouseResult.Value);

        await context.SaveAsync(ct);

        return warehouseId.Value;
    }
}
