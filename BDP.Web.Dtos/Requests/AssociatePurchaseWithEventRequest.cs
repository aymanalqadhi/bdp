using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class AssociatePurchaseWithEventRequest
{
    /// <summary>
    /// Gets or sets the id of the purchase to associate
    /// </summary>
    [Required]
    public Guid PurchaseId { get; set; }
}