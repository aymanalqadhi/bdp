namespace BDP.Web.Dtos;

public class FinancialRecordDto
{
    /// <summary>
    /// Gets or sets the financial record id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the amount associated with the financial record
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the verification of the record
    /// </summary>
    public FinancialRecordVerificationDto? Verification { get; set; }

    /// <summary>
    /// Gets or sets the user associated with the financial record
    /// </summary>
    public UserDto? MadeBy { get; set; }

    /// <summary>
    /// An optinal note set by the user
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Gets or sets the creation date/time of this record
    /// </summary>
    public DateTime CreatedAt { get; set; }
}