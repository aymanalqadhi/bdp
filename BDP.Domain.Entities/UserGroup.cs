namespace BDP.Domain.Entities;

/// <summary>
/// A recrod to represent keys for the <see cref="UserGroup"/> entity
/// </summary>
/// <param name="Id">The id field of the key</param>
public sealed record UserGroupKey(Guid Id) : EntityKey(Id);

public sealed class UserGroup : AuditableEntity
{
    /// <summary>
    /// Gets or sets the id of the user group
    /// </summary>
    public UserGroupKey Id { get; set; } = new UserGroupKey(Guid.NewGuid());

    /// <summary>
    /// Gets or sets the name of the group
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the list of users enrolled in this group
    /// </summary>
    public ICollection<User> Users { get; set; } = new List<User>();
}