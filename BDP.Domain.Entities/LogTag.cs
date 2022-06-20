namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a log tag
/// </summary>
public sealed class LogTag : AuditableEntity<LogTag>
{
    #region Properties

    /// <summary>
    /// Gets or sets the collection of logs associated with this tag
    /// </summary>
    public ICollection<Log> Logs { get; set; } = new List<Log>();

    /// <summary>
    /// Gets or sets the message of the log
    /// </summary>
    public string Value { get; set; } = null!;

    #endregion Properties
}