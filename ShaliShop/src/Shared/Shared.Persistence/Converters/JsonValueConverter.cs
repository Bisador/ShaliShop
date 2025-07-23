using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Shared.Persistence.Converters;

public class JsonValueConverter<T> : ValueConverter<T, string>
{
    public JsonValueConverter(JsonSerializerOptions? options = null)
        : base(
            v => Serialize(options, v),
            v => Deserialize(v, options ?? JsonDefaults.Options)
        )
    {
    }

    private static string Serialize(JsonSerializerOptions? options, T v)
    {
        return JsonSerializer.Serialize(v, options ?? JsonDefaults.Options);
    }

    private static T Deserialize(string json, JsonSerializerOptions options)
    {
        var result = JsonSerializer.Deserialize<T>(json, options);
        if (result is null)
            throw new JsonException($"Failed to deserialize into {typeof(T).Name}");
        return result;
    }
}