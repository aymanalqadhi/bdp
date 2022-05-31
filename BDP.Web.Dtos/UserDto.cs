namespace BDP.Web.Dtos;

public class UserDto
{
    /// <summary>
    /// Gets or sets the id of the user
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Gets or sets the email of the user
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Gets or sets the full name of the user
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Gets or sets the bio of the user
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// Gets or sets the location of the user
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets whether the user is active or not
    /// </summary>
    public bool IsActive { get; set; } = false;

    /// <summary>
    /// Gets or sets whether the user is active or not
    /// </summary>
    public bool IsConfirmed { get; set; } = false;

    /// <summary>
    /// Gets or sets whether the user has been verified
    /// </summary>
    public bool IsVerified { get; set; } = false;

    /// <summary>
    /// Gets or sets the url to the profile picture of the user
    /// </summary>
    public string? ProfilePictureUrl { get; set; }

    /// <summary>
    /// Gets or sets the creation time of the user
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the url to the cover picture of the user
    /// </summary>
    public string? CoverPictureUrl { get; set; }

    /// <summary>
    /// Gets or sets the list of phone numbers of the user
    /// </summary>
    public ICollection<string>? PhoneNumbers { get; set; }

    /// <summary>
    /// Gets or sets the list of user groups that the user is enrolled in
    /// </summary>
    public ICollection<string> Groups { get; set; } = new List<string>();
}