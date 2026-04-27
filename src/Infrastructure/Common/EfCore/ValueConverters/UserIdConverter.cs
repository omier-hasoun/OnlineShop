
using Domain.Customers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Common.EfCore.ValueConverters;

public sealed class UserIdConverter : ValueConverter<CustomerId, Guid>
{
    public UserIdConverter()
        : base(id => id.Value, value => new CustomerId(value))
    {
    }

}
