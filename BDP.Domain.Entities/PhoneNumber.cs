namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a phone number owned by a user
/// </summary>
public sealed class PhoneNumber : AuditableEntity
{
    /// <summary>
    /// Gets or sets the phone number value
    /// </summary>
    public string Number { get; set; } = null!;
}
