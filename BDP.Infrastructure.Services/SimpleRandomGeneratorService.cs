using BDP.Domain.Services;
using System.Text;

namespace BDP.Infrastructure.Services;

public class SimpleRandomGeneratorService : IRandomGeneratorService
{
    private static readonly Random _rnd = new();

    private const string _uppercasePool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string _lowercasePool = "abcdefghijklmnopqrstuvwxyz";
    private const string _alphaPool = _uppercasePool + _lowercasePool;
    private const string _numPool = "0123456789";
    private const string _alphanumPool = _alphaPool + _numPool;
    private const string _symbolPool = "`~!@#$%^&*()-=_+,./<>?;':\"[]\\{}|";
    private const string _allPool = _alphanumPool + _symbolPool;

    /// <inheritdoc/>
    public int RandomInt(int low = int.MinValue, int high = int.MaxValue)
        => _rnd.Next(low, high);

    /// <inheritdoc/>
    public string RandomString(int length, RandomStringKind kind = RandomStringKind.AlphaNum)
    {
        if (kind == RandomStringKind.All)
            return RandomStringFromPool(length, _allPool);

        if (kind == RandomStringKind.AlphaNum)
            return RandomStringFromPool(length, _alphanumPool);

        if (kind == RandomStringKind.Alpha)
            return RandomStringFromPool(length, _alphaPool);

        var pool = new StringBuilder();

        if ((kind & RandomStringKind.LowercaseCharacters) != RandomStringKind.None)
            pool.Append(_lowercasePool);

        if ((kind & RandomStringKind.UppercaseCharacters) != RandomStringKind.None)
            pool.Append(_uppercasePool);

        if ((kind & RandomStringKind.Numbers) != RandomStringKind.None)
            pool.Append(_numPool);

        if ((kind & RandomStringKind.Symbols) != RandomStringKind.None)
            pool.Append(_symbolPool);

        return RandomStringFromPool(length, pool.ToString());
    }

    public static string RandomStringFromPool(int length, string pool)
    {
        var builder = new StringBuilder();

        for (var i = 0; i < length; i++)
            builder.Append(pool[_rnd.Next(pool.Length)]);

        return builder.ToString();
    }
}