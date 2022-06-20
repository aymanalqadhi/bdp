namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a service reservation
/// </summary>
public sealed class ServiceReservation : Purchase<ServiceReservation>
{
    /// <summary>
    /// Gets or sets the service of the reservation
    /// </summary>
    public Service Service { get; set; } = null!;
}