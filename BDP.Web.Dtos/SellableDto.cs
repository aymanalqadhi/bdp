namespace BDP.Web.Dtos;

public class SellableDto
{
    /// <summary>
    /// Gets or sets the id of the user
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the sellable
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the sellable
    /// </summary
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the price of the sellable
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the user who listed this sellable
    /// </summary>
    public UserDto OfferedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets whether the sellable is available
    /// </summary>
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Gets or sets the list of attachments attached to the sellable
    /// </summary>
    public IEnumerable<string>? Attachments { get; set; }

    /// <summary>
    /// Gets or sets the creation time of the user
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets
    /// </summary>
    public DateTime ModifiedAt { get; set; }
}