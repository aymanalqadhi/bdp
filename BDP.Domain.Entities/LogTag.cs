namespace BDP.Domain.Entities;

/// <summary>
/// A recrod to represent keys for the <see cref="LogTag"/> entity
/// </summary>
/// <param name="Id">The id field of the key</param>
public sealed record LogTagKey(Guid Id) : EntityKey<LogTag>(Id);

/// <summary>
/// A class to represent a log tag
/// </summary>
public sealed class LogTag : AuditableEntity
{
    #region Properties

    /// <summary>
    /// Gets or sets the id of the log tag
    /// </summary>
    public LogTagKey Id { get; set; } = new LogTagKey(Guid.NewGuid());

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