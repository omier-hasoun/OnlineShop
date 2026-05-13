using System.Text.Json;

namespace Api.Common.JsonConverters;

public sealed class NullableLongAsStringJsonConverter : JsonConverter<long?>
{
    public override long? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        switch (reader.TokenType)
        {
            case JsonTokenType.Number:
                if (reader.TryGetInt64(out var longValue))
                    return longValue;

                break;

            case JsonTokenType.String:
                var str = reader.GetString();

                if (string.IsNullOrWhiteSpace(str))
                    return null;

                if (long.TryParse(str, out var parsed))
                    return parsed;

                break;
        }

        throw new JsonException(
            $"Unable to convert value to nullable {nameof(Int64)}.");
    }

    public override void Write(
        Utf8JsonWriter writer,
        long? value,
        JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString());
        else
            writer.WriteNullValue();
    }
}
