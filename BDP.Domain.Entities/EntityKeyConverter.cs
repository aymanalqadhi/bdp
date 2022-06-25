using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;

namespace BDP.Domain.Entities;

public class EntityKeyConverter<TEntity> : TypeConverter
    where TEntity : class
{
    #region Fields

    [SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "The field is used via reflection")]
    private readonly Type _type;

    #endregion Fields

    #region Public Constructors

    public EntityKeyConverter(Type type)
    {
        _type = type;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string)
            || base.CanConvertFrom(context, sourceType);
    }

    /// <inheritdoc/>
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        return destinationType == typeof(string)
            || base.CanConvertTo(context, destinationType);
    }

    /// <inheritdoc/>
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string s)
            return new EntityKey<TEntity>(Guid.Parse(s));

        return base.ConvertFrom(context, culture, value);
    }

    /// <inheritdoc/>
    public override object? ConvertTo(
        ITypeDescriptorContext? context,
        CultureInfo? culture,
        object? value, Type destinationType)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        if (destinationType == typeof(string))
            return ((EntityKey<TEntity>)value).ToString();

        return base.ConvertTo(context, culture, value, destinationType);
    }

    #endregion Public Methods
}

/// <inheritdoc/>
public class EntityKeyConverter : TypeConverter
{
    #region Fields

    private static readonly ConcurrentDictionary<Type, TypeConverter> _actualConverters = new();

    private readonly TypeConverter _innerConverter;

    #endregion Fields

    #region Public Constructors

    public EntityKeyConverter(Type stronglyTypedIdType)
    {
        _innerConverter = _actualConverters.GetOrAdd(stronglyTypedIdType, CreateActualConverter);
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => _innerConverter.CanConvertFrom(context, sourceType);

    /// <inheritdoc/>
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        => _innerConverter.CanConvertTo(context, destinationType);

    /// <inheritdoc/>
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        => _innerConverter.ConvertFrom(context, culture, value);

    /// <inheritdoc/>
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        => _innerConverter.ConvertTo(context, culture, value, destinationType);

    #endregion Public Methods

    #region Private Methods

    /// <inheritdoc/>
    private static TypeConverter CreateActualConverter(Type keyType)
    {
        if (!EntityKeyHelper.IsStronglyTypedId(keyType, out var entityType))
            throw new InvalidOperationException($"The type '{keyType}' is not a strongly typed id");

        var actualConverterType = typeof(EntityKeyConverter<>).MakeGenericType(entityType);

        return (TypeConverter)Activator.CreateInstance(actualConverterType, keyType)!;
    }

    #endregion Private Methods
}

public static class EntityKeyHelper
{
    #region Fields

    private static readonly ConcurrentDictionary<Type, Delegate> _keyFactories = new();

    #endregion Fields

    #region Public Methods

    public static Func<TValue, object> GetFactory<TValue>(Type keyType)
        where TValue : notnull
    {
        return (Func<TValue, object>)_keyFactories.GetOrAdd(
            keyType,
            CreateFactory<TValue>);
    }

    public static bool IsStronglyTypedId(Type type) => IsStronglyTypedId(type, out _);

    public static bool IsStronglyTypedId(Type type, [NotNullWhen(true)] out Type? entityType)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        if (type.IsGenericType &&
            type.GetGenericTypeDefinition() == typeof(EntityKey<>))
        {
            entityType = type.GetGenericArguments()[0];

            return true;
        }

        entityType = null;

        return false;
    }

    #endregion Public Methods

    #region Private Methods

    private static Func<TValue, object> CreateFactory<TValue>(Type keyType)
                where TValue : notnull
    {
        if (!IsStronglyTypedId(keyType))
            throw new ArgumentException($"Type '{keyType}' is not a strongly-typed id type", nameof(keyType));

        var ctor = keyType.GetConstructor(new[] { typeof(TValue) });

        if (ctor is null)
        {
            throw new ArgumentException(
                $"Type '{keyType}' doesn't have a constructor with one parameter of type '{typeof(TValue)}'",
                nameof(keyType));
        }

        var param = Expression.Parameter(typeof(TValue), "value");
        var body = Expression.New(ctor, param);
        var lambda = Expression.Lambda<Func<TValue, object>>(body, param);

        return lambda.Compile();
    }

    #endregion Private Methods
}