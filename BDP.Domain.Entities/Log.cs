namespace BDP.Domain.Entities;

/// <summary>
/// A recrod to represent keys for the <see cref="Log"/> entity
/// </summary>
/// <param name="Id">The id field of the key</param>
public sealed record LogKey(Guid Id) : EntityKey<Log>(Id);

/// <summary>
/// A class to represent a log
/// </summary>
public sealed class Log : AuditableEntity
{
    #region Properties

    /// <summary>
    /// Gets or sets the id of the log
    /// </summary>
    public LogKey Id { get; set; } = new LogKey(Guid.NewGuid());

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