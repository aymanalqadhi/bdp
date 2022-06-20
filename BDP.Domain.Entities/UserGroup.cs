namespace BDP.Domain.Entities;

public sealed class UserGroup : AuditableEntity<UserGroup>
{
    /// <summary>
    /// Gets or sets the name of the group
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the list of users enrolled in this group
    /// </summary>
    public ICollection<User> Users { get; set; } = new List<User>();
}