using BDP.Tests.Util;

namespace BDP.Domain.Entities.Validators.Tests;

public static class ValidEntitiesFactory
{
    /// <summary>
    /// Creates a valid user object
    /// </summary>
    /// <returns></returns>
    public static User CreateUser()
    {
        var alphaTextBlock = (int max) => RandomGenerator.NextString(max, RandomStringOptions.Lowercase);

        return new User
        {
            Username = alphaTextBlock(8),
            Email = $"{alphaTextBlock(10)}@{alphaTextBlock(6)}.{alphaTextBlock(5)}",
        };
    }

    /// <summary>
    /// Creates a valid log object
    /// </summary>
    /// <returns></returns>
    public static Log CreateLog()
    {
        return new Log
        {
            Severity = LogSeverity.Error,
        };
    }
}