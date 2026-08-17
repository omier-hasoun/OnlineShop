using Domain.Common.ValueObjects;
using Shippo;
using Shippo.Models.Components;
using Nager.Date;

namespace Infrastructure.Services.Shipping.Shippo;

public sealed class ShippoService(IShippoSDK shippo) : IShippingService
{
    public async Task<Result<Shipment>> CreateShipmentAsync(AddressDetails from, string CompanyEmail, AddressDetails to, string customerEmail)
    {
        throw new NotImplementedException();
        var addressFrom = new AddressFrom(AddressFromType.AddressCreateRequest)
        {
            AddressCreateRequest = new AddressCreateRequest()
            {
                Name = from.FullName,
                Company = from.Company,
                Street1 = from.AddressLine1,
                Street2 = from.AddressLine2,
                StreetNo = from.HouseNo,
                City = from.City,
                State = from.StateProvince,
                Zip = from.PostalCode,
                Country = from.Country,
                Phone = from.PhoneNumber,
                Email = CompanyEmail,
                IsResidential = true,
                Validate = false,

            }
        };

        var addressTo = new AddressTo(AddressToType.AddressCreateRequest)
        {
            AddressCreateRequest = new AddressCreateRequest()
            {
                Name = to.FullName,
                Company = to.Company,
                Street1 = to.AddressLine1,
                Street2 = to.AddressLine2,
                StreetNo = to.HouseNo,
                City = to.City,
                State = to.StateProvince,
                Zip = to.PostalCode,
                Country = to.Country,
                Phone = to.PhoneNumber,
                Email = customerEmail,
                IsResidential = true,
                Validate = false,

            }
        };

        var s = new ShipmentCreateRequest
        {
            Async = false,
            AddressFrom = addressFrom,
            AddressTo = addressTo,
            ShipmentDate = GetNextWorkingDay(Enum.Parse<CountryCode>(from.Country))
                                          .ToString("o"),
            

        };
        
        //// Create a shipment using the Shippo SDK
        //var shipment = await shippo.Shipments.CreateAsync();

    }

    private static DateTime GetNextWorkingDay(CountryCode countryCode)
    {
        var date = DateTime.UtcNow.AddDays(1);
        while (!IsWorkingDay(date, countryCode))
        {
            date = date.AddDays(1);
        }
        return date;
    }

    private static bool IsWorkingDay(DateTime date, CountryCode countryCode)
    {
        return !HolidaySystem.IsPublicHoliday(date, countryCode) &&
               !WeekendSystem.IsWeekend(date, countryCode);
    }
}
