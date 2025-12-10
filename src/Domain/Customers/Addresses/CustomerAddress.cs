
namespace Domain.Customers.Addresses;

public sealed class CustomerAddress : BaseEntity
{


    private CustomerAddress()
    {

    }

    public static CustomerAddress Create(string country, string city, string street, string zipcode, CustomerId customerId, CustomerAddressId id = default)
    {
        return new()
        {
            Id = id,
            Country = country,
            City = city,
            Street = street,
            Zipcode = zipcode,
            CustomerId = customerId,
        };
    }

    public CustomerAddressId Id { get; private init; }
    public string Country { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string Street { get; private set; } = null!;
    public string Zipcode { get; private set; } = null!;
    public CustomerId CustomerId { get; private set; }
}
