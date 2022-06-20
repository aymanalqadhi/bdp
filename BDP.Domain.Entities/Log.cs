namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a log
/// </summary>
public sealed class Log : AuditableEntity<Log>
{
    #region Properties

    /// <summary>
    /// Gets or sets the message of the log
    /// </summary>
    public string Message { get; set; } = null!;

    /// <summary>
    /// Gets or sets the log severity level
    /// </summary>
    public LogSeverity Severity { get; set; }

    /// <summary>
    /// Gets or sets the tags set of the log
    /// </summary>
    public ICollection<LogTag> Tags { get; set; } = new List<LogTag>();

    /// <summary>
    /// Gets or sets the timestamp of the log
    /// </summary>
    public DateTime TimeStamp { get; set; }

    #endregion Properties
}

/// <summary>
/// An enum to represent the log severity levels
/// </summary>
public enum LogSeverity : int
{
    Fatal = 0,
    Error = 1,
    Warning = 2,
    Information = 3,
    Debug = 4,
}