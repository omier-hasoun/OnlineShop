using Application.Services.Shipping;
using Shippo;
using Shippo.Models.Components;
namespace Infrastructure.ExternalServices.ShippingService.Shippo;

public sealed class ShippoService(IShippoSDK shippo) : IShippingGateway
{
    public Task<Result<Domain.Orders.Shipments.Shipment>> CreateShipmentAsync()
    {
        //Shipment shipment = shippo.Shipments.CreateAsync();

        //shipment.

        throw new NotImplementedException();
    }
}
