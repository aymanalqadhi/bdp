namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a service offered by a user
/// </summary>
/// <typeparam name="TKey">The key type of the service</typeparam>
public sealed class Service : Sellable<Service, ServiceReview>
{
    /// <summary>
    /// Gets or sets the availability begining time of the service
    /// (to be changed with <see cref="TimeOnly"/>)
    /// </summary>
    public DateTime AvailableBegin { get; set; }

    /// <summary>
    /// Gets or sets the availability begining time of the service
    /// (to be changed with <see cref="TimeOnly"/>)
    /// </summary>
    public DateTime AvailableEnd { get; set; }
}