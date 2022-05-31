namespace BDP.Web.Dtos;

public class TransactionDto
{
    /// <summary>
    /// Gets or sets the id of the transaction
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the amount set by the transaction
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the source of the transaction
    /// </summary>
    public UserDto From { get; set; } = null!;

    /// <summary>
    /// Gets or sets the destination of the transaction
    /// </summary>
    public UserDto To { get; set; } = null!;

    /// <summary>
    /// Gets or sets the confimation token of the transaction
    /// </summary>
    public string? ConfirmationToken { get; set; }

    /// <summary>
    /// Gets or sets the confimation of the request
    /// </summary>
    public TransactionConfirmationDto? Confirmation { get; set; }

    /// <summary>
    /// Gets or sets the date at which the transaction was made
    /// </summary>
    public DateTime CreatedAt { get; set; }
}