namespace BDP.Domain.Services;

public interface IRandomGeneratorService
{
    /// <summary>
    /// Generates a random string value
    /// </summary>
    /// <param name="length">The length of the string</param>
    /// <param name="kind">The kind of the string characters</param>
    /// <returns></returns>
    string RandomString(int length, RandomStringKind kind = RandomStringKind.AlphaNum);

    /// <summary>
    /// Generates a random integer number
    /// </summary>
    /// <param name="low">Lower bound</param>
    /// <param name="high">Higher bound</param>
    /// <returns></returns>
    int RandomInt(int low = int.MinValue, int high = int.MaxValue);
}

[Flags]
public enum RandomStringKind
{
    None = 0,

    UppercaseCharacters = 1,
    LowercaseCharacters = 2,
    Alpha = UppercaseCharacters | LowercaseCharacters,

    Numbers = 4,
    AlphaNum = Alpha | Numbers,

    Symbols = 8,

    All = AlphaNum | Symbols,
}