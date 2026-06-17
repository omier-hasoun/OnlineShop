
using Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Common.EfCore.ValueConverters;

internal class MoneyConverter : ValueConverter<Money, decimal>
{
    private static readonly ConverterMappingHints _mappingHints = new ConverterMappingHints(null, precision: 18, scale: 4);
    public MoneyConverter() : base(x => x.Value, value => Money.Create(value), _mappingHints)
    {
    }
}
