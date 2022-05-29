namespace BDP.Domain.Entities;

public class Confirmation : AuditableEntity
{
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
