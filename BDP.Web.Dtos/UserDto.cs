using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A data-transfer object for <see cref="User"/>
/// </summary>
public sealed class UserDto : EntityDto<User>
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
    /// Gets or sets whether the user is active or not
    /// </summary>
    public bool IsActive { get; set; } = false;

    /// <summary>
    /// Gets or sets the role of the user
    /// </summary>
    public string Role { get; set; } = null!;
}