namespace BDP.Web.Dtos;

public class PurchaseDto
{
    /// <summary>
    /// Gets or sets the id of the purchase
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the transaction associated with the purchase
    /// </summary>
    public TransactionDto Transaction { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date of the purchase
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

public sealed class OrderDto : PurchaseDto
{
    /// <summary>
    /// Gets or sets the quantity of the order
    /// </summary>
    public uint Quantity { get; set; }

    /// <summary>
    /// Gets or sets the product of the order
    /// </summary>
    public ProductDto Product { get; set; } = null!;
}

public sealed class ReservationDto : PurchaseDto
{
    /// <summary>
    /// Gets or sets the service of the reservation
    /// </summary>
    public ServiceDto Service { get; set; } = null!;
}