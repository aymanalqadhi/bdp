namespace BDP.Domain.Entities;

public class UserProfile : AuditableEntity<UserProfile>
{
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
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the user
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets whether the user has been verified
    /// </summary>
    public bool IsVerified { get; set; } = false;

    /// <summary>
    /// Gets or sets the profile picture of the user
    /// </summary>
    public Attachment? ProfilePicture { get; set; }

    /// <summary>
    /// Gets or sets the cover picture of the user
    /// </summary>
    public Attachment? CoverPicture { get; set; }

    /// <summary>
    /// Gets or sets the id of the owner of the profile
    /// </summary>
    public EntityKey<User> UserId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the owner of the profile
    /// </summary>
    public User User { get; set; } = null!;
}