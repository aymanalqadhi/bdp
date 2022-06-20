namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a user refresh token
/// </summary>
public sealed class RefreshToken : AuditableEntity<RefreshToken>
{
    /// <summary>
    /// Gets or sets the device unique identifier
    /// </summary>
    public string UniqueIdentifier { get; set; } = null!;

    /// <summary>
    /// Gets or sets the last ip address that used the token
    /// </summary>
    public string LastIpAddress { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date and time of the last login
    /// </summary>
    public DateTime LastLogin { get; set; }

    /// <summary>
    /// Gets or sets the device name of the device that uses the token
    /// </summary>
    public string? DeviceName { get; set; }

    /// <summary>
    /// Gets or sets the hostname of the device that uses the token
    /// </summary>
    public string? HostName { get; set; }

    /// <summary>
    /// Gets or sets the token string
    /// </summary>
    public string Token { get; set; } = null!;

    /// <summary>
    /// Gets or sets the expiration date of the token
    /// </summary>
    public DateTime ValidUntil { get; set; }

    /// <summary>
    /// Gets or sets the owner user
    /// </summary>
    public User Owner { get; set; } = null!;
}