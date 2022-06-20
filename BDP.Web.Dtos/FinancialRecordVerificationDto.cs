namespace BDP.Web.Dtos;

public class FinancialRecordVerificationDto
{
    /// <summary>
    /// Gets or sets the id of the verification
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets whether the record has been accepted
    /// </summary>
    public bool IsAccepted { get; set; }

    /// <summary>
    /// Gets or sets the document of the record verification
    /// </summary>
    public string? Document { get; set; } = null!;

    /// <summary>
    /// Gets or sets additional notes on verifications
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation date/time of this verification
    /// </summary>
    public DateTime CreatedAt { get; set; }
}