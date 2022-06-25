namespace BDP.Domain.Entities;

/// <summary>
/// An interface to be implemented by ownable entities
/// </summary>
public interface IOwnable
{
    /// <summary>
    /// Gets the user who owns the resoruce
    /// </summary>
    User OwnedBy { get; }
}