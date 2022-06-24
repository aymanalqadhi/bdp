using BDP.Domain.Entities;

using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Concurrent;

namespace BDP.Web.Dtos.Converters;

/// <inheritdoc/>
public class EntityKeyJsonConverter<TKey, TEntity> : JsonConverter<TKey>
    where TEntity : class
    where TKey : EntityKey<TEntity>
{
    /// <inheritdoc/>
    public override TKey? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType is JsonTokenType.Null)
            return default;

        var value = JsonSerializer.Deserialize<Guid>(ref reader, options);
        var factory = EntityKeyHelper.GetFactory<Guid>(typeToConvert);

        return (TKey)factory(value);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, TKey value, JsonSerializerOptions options)
    {
        if (value is null)
            writer.WriteNullValue();
        else
            JsonSerializer.Serialize(writer, value.Value, options);
    }
}

/// <inheritdoc/>
public class EntityKeyJsonConverterFactory : JsonConverterFactory
{
    private static readonly ConcurrentDictionary<Type, JsonConverter> _cache = new();

    /// <inheritdoc/>
    public override bool CanConvert(Type typeToConvert)
    {
        return EntityKeyHelper.IsStronglyTypedId(typeToConvert);
    }

    /// <inheritdoc/>
    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        return _cache.GetOrAdd(typeToConvert, CreateConverter!);
    }

    private static JsonConverter? CreateConverter(Type typeToConvert)
    {
        if (!EntityKeyHelper.IsStronglyTypedId(typeToConvert, out var valueType))
            throw new InvalidOperationException($"Cannot create converter for '{typeToConvert}'");

        var type = typeof(EntityKeyJsonConverter<,>).MakeGenericType(typeToConvert, valueType);

        return Activator.CreateInstance(type) as JsonConverter;
    }
}