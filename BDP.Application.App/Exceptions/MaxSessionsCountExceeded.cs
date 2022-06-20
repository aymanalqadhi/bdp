namespace BDP.Application.App.Exceptions;

public sealed class MaxSessionsCountExceeded : Exception
{
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="maxSessionsCount">The maximum allowed sessions count</param>
    public MaxSessionsCountExceeded(int maxSessionsCount)
        : base($"only ${maxSessionsCount} sessions are allowed by user")
    {
    }
}
