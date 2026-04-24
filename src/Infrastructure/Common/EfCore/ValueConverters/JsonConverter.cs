
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Common.EfCore.ValueConverters;

internal sealed class JsonConverter<T> : ValueConverter<T,string>
{
    public JsonConverter()
        : base(     
            (T value) => JsonSerializer.Serialize(value, (JsonSerializerOptions)null!),
            (string value) => JsonSerializer.Deserialize<T>(value, (JsonSerializerOptions)null!)!)
    {

    }

}
