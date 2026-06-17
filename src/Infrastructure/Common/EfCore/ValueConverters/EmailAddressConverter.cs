
using Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Common.EfCore.ValueConverters;

internal sealed class EmailAddressConverter : ValueConverter<EmailAddress, string>
{
    public EmailAddressConverter() : base(e => e.Value, value => EmailAddress.Create(value).Value)
    {
        
    }
}
