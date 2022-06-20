namespace BDP.Domain.Entities;

/// <summary>
/// A recrod to represent keys for the <see cref="Confirmation"/> entity
/// </summary>
/// <param name="Id">The id field of the key</param>
public sealed record ConfirmationKey(Guid Id) : EntityKey<Confirmation>(Id);

/// <summary>
/// A class to represent a user account confirmation
/// </summary>
public class Confirmation : AuditableEntity
{
    /// <summary>
    /// Gets or sets the id of the confirmation
    /// </summary>
    public ConfirmationKey Id { get; set; } = new ConfirmationKey(Guid.NewGuid());

    /// <summary>
    /// Gets or sets the OTP of the confirmation
    /// </summary>
    public string OneTimePassword { get; set; } = null!;

    /// <summary>
    /// Gets or sets the token of the confirmation
    /// </summary>
    public string Token { get; set; } = null!;

    /// <summary>
    /// Gets or sets the validity period of the confirmation
    /// </summary>
    public TimeSpan ValidFor { get; set; }

    /// <summary>
    /// Gets or sets the user to whom the confirmation belongs
    /// </summary>
    public User ForUser { get; set; } = null!;
}