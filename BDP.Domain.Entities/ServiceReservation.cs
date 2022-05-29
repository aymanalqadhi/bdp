namespace BDP.Domain.Entities;

public sealed class ServiceReservation : Purchase
{
    /// <summary>
    /// Gets or sets the service of the reservation
    /// </summary>
    public Service Service { get; set; } = null!;
}