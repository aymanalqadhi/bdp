namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a service offered by a user
/// </summary>
/// <typeparam name="TKey">The key type of the service</typeparam>
public sealed class Service : Sellable<Service>
{
    /// <summary>
    /// Gets or sets the availability begining time of the service
    /// </summary>
    public TimeOnly AvailableBegin { get; set; }

    /// <summary>
    /// Gets or sets the availability begining time of the service
    /// </summary>
    public TimeOnly AvailableEnd { get; set; }

    /// <summary>
    /// Gets or sets the collection of reviews
    /// </summary>
    public ICollection<ServiceReview> Reviews { get; set; } = new List<ServiceReview>();
}