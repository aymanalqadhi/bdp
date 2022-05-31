using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class ConfirmTransactionRequest
{
    /// <summary>
    /// Gets or sets the token that is used to confirm the transaction
    /// </summary>
    [Required]
    public string Token { get; set; } = null!;
}