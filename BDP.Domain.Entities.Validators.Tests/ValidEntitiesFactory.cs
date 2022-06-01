namespace BDP.Domain.Entities.Validators.Tests;

public static class ValidEntitiesFactory
{
    /// <summary>
    /// Creates a valid log entites
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