using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class DepositRequest
{
    /// <summary>
    /// Gets or sets the amount to deposit
    /// </summary>
    [Required]
    [Range(1, 1_000_000)]
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or set an optinal note left by the user
    /// </summary>
    [MaxLength(255)]
    public string? Note { get; set; }
}