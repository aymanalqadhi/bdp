using BDP.Domain.Entities;

using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Concurrent;

namespace BDP.Web.Dtos.Converters;

/// <inheritdoc/>
public class StronglyTypedIdJsonConverter<TEntity, TValue>
    : JsonConverter<EntityKey<TEntity, TValue>>
{
    /// <inheritdoc/>
    public override EntityKey<TEntity, TValue>? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType is JsonTokenType.Null)
            return null;

        var value = JsonSerializer.Deserialize<TValue>(ref reader, options);

        return new EntityKey<TEntity, TValue>(value!);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, EntityKey<TEntity, TValue> value, JsonSerializerOptions options)
    {
        if (value is null)
            writer.WriteNullValue();
        else
            JsonSerializer.Serialize(writer, value.Id, options);
    }
}

/// <inheritdoc/>
public class StronglyTypedIdJsonConverter<TEntity>
    : StronglyTypedIdJsonConverter<TEntity, Guid>
{
}