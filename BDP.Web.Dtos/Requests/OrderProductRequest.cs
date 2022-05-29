using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class OrderProductRequest
{
    [Required]
    [Range(1, 100, ErrorMessage = "you cannot order less than 1 or more than 100 products")]
    public uint Quantity { get; set; }
}
