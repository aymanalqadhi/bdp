namespace BDP.Domain.Entities;

/// <summary>
/// A recrod to represent keys for the <see cref="PhoneNumber"/> entity
/// </summary>
/// <param name="Id">The id field of the key</param>
public sealed record PhoneNumberKey(Guid Id) : EntityKey<PhoneNumber>(Id);

/// <summary>
/// A class to represent a phone number owned by a user
/// </summary>
public sealed class PhoneNumber : AuditableEntity
{
    /// <summary>
    /// Gets or sets the id of the phone number
    /// </summary>
    public PhoneNumberKey Id { get; set; } = new PhoneNumberKey(Guid.NewGuid());

    /// <summary>
    /// Gets or sets the phone number value
    /// </summary>
    public string Number { get; set; } = null!;
}