

using System.Text;

namespace Domain.Common.ValueObjects;

public sealed record AddressDetails
{
    private AddressDetails()
    {
        
    }
    public AddressDetails(string fullName, string phoneNumber, string country, string? houseNo,
        string city, string postalCode, string addressLine1, string? addressLine2 = null, string? stateProvince = null,
        string? notes = null)
    {
        FullName = fullName ?? throw new ArgumentNullException();
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException();
        Country = country ?? throw new ArgumentNullException();
        City = city ?? throw new ArgumentNullException(); 
        PostalCode = postalCode ?? throw new ArgumentNullException(); 
        AddressLine1 = addressLine1 ?? throw new ArgumentNullException(); 

        HouseNo = houseNo;
        AddressLine2 = addressLine2;
        StateProvince = stateProvince;
        Notes = notes;
    }

    public string FullName { get; }
    public string PhoneNumber { get; }
    public string Country { get; }
    public string City { get; }
    public string PostalCode { get; }
    public string AddressLine1 { get; }
    public string? HouseNo { get; }
    public string? AddressLine2 { get; }
    public string? StateProvince { get; }
    public string? Notes { get; }

    public override string ToString()
    {
        var address = new StringBuilder();

        address.AppendLine(FullName);

        if (!string.IsNullOrWhiteSpace(AddressLine1))
        {
            address.Append(AddressLine1);

            if (!string.IsNullOrWhiteSpace(HouseNo))
                address.Append($" {HouseNo}");

            address.AppendLine();
        }

        if (!string.IsNullOrWhiteSpace(AddressLine2))
            address.AppendLine(AddressLine2);

        address.AppendLine($"{PostalCode} {City}");

        if (!string.IsNullOrWhiteSpace(StateProvince))
            address.AppendLine(StateProvince);

        address.AppendLine(Country);

        if (!string.IsNullOrWhiteSpace(PhoneNumber))
            address.AppendLine($"Phone: {PhoneNumber}");

        if (!string.IsNullOrWhiteSpace(Notes))
            address.AppendLine($"Notes: {Notes}");

        return address.ToString().Trim();
    }
}
