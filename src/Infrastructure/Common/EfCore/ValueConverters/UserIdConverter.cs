
using Domain.Customers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Common.EfCore.ValueConverters;

public sealed class UserIdConverter : ValueConverter<UserId, Guid>
{
    public UserIdConverter()
        : base(id => id.Value, value => new UserId(value))
    {
    }

}
