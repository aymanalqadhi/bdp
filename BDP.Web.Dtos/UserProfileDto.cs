using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A data-transfer object for <see cref="UserProfile"/>
/// </summary>
public sealed class UserProfileDto : MutableEntityDto<UserProfile>
{
    /// <summary>
    /// Gets or sets the full name of the user
    /// </summary>
    public string FullName { get; set; } = null!;

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
    public Uri? ProfilePicture { get; set; }

    /// <summary>
    /// Gets or sets the cover picture of the user
    /// </summary>
    public Uri? CoverPicture { get; set; }

    /// <summary>
    /// Gets or sets the owner of the profile
    /// </summary>
    public UserDto User { get; set; } = null!;
}