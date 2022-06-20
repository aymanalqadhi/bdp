namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a user of the system
/// </summary>
public sealed class User : AuditableEntity<User>
{
    /// <summary>
    /// Gets or sets the username of the user
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Gets or sets the email of the user
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Gets or sets the hash of the password of the user
    /// </summary>
    public string PasswordHash { get; set; } = null!;

    /// <summary>
    /// Gets or sets the full name of the user
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Gets or sets the user's bio
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// Gets or sets the location of the user
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the user
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets whether the user is active or not
    /// </summary>
    public bool IsActive { get; set; } = false;

    /// <summary>
    /// Gets or sets whether the user is confirmed or not
    /// </summary>
    public bool IsConfirmed { get; set; } = false;

    /// <summary>
    /// Gets or sets whether the user has been verified
    /// </summary>
    public bool IsVerified { get; set; } = false;

    /// <summary>
    /// Gets or sets the role of the user
    /// </summary>
    public UserRole Role { get; set; } = UserRole.None;

    /// <summary>
    /// Gets or sets the profile picture of the user
    /// </summary>
    public Attachment? ProfilePicture { get; set; }

    /// <summary>
    /// Gets or sets the cover picture of the user
    /// </summary>
    public Attachment? CoverPicture { get; set; }
}

/// <summary>
/// An enum to represent user roles
/// </summary>
public enum UserRole : uint
{
    None = 0,
    Customer = 1,
    Provider = 2,
    Admin = 4,
    Root = 0xFFFFFFFF,
}