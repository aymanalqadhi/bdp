namespace BDP.Web.Dtos;

public class TransactionConfirmationDto
{
    /// <summary>
    /// Gets or sets the id of the confirmation
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets whether the transaction has been accepted or not
    /// </summary>
    public bool IsAccepted { get; set; }

    /// <summary>
    /// Gets or sets the date at which the transaction was confirmed
    /// </summary>
    public DateTime CreatedAt { get; set; }
}