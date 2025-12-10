
namespace Domain.Customers;

public sealed class Customer : BaseEntity
{
    private Customer()
    {

    }

    public static Customer Create(string firstName, string lastName, string email, string phoneNumber, CustomerId id = default)
    {



        return new()
        {
            Id = id == default ? new CustomerId(Guid.CreateVersion7()) : id,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Email = email
        };
    }

    public CustomerId Id { get; private init; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string? PhoneNumber { get; private set; } = null!;

    public CustomerAddress? Address { get; private set; } = null!;
    public ICollection<CartItem> CartItems { get; private set; } = [];

}
