
using Domain.Customers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Common.EfCore.ValueConverters;

public sealed class CustomerIdConverter : ValueConverter<CustomerId, Guid>
{
    public CustomerIdConverter()
        : base(id => id.Value, value => new CustomerId(value))
    {
    }

}
