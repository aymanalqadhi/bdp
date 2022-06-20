using BDP.Domain.Entities;

namespace BDP.Domain.Services;

public interface IServicesService
{
    /// <summary>
    /// Asynchronously gets a service by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Service> GetByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously lists a service
    /// </summary>
    /// <param name="title">The title of the product</param>
    /// <param name="description">The description of the product</param>
    /// <param name="price">The price of the product</param>
    /// <param name="availableBegin">The starting availability time of the service</param>
    /// <param name="availableEnd">The ending availability time of the service</param>
    /// <param name="attachments">The attachments of the service</param>
    /// <returns>The listed service</returns>
    Task<Service> ListAsync(
        User user,
        string title,
        string description,
        decimal price,
        DateTime availableBegin,
        DateTime availableEnd,
        IEnumerable<IUploadFile>? attachments = null
    );

    /// <summary>
    /// Asynchronsoulsy updates a product
    /// </summary>
    /// <param name="service">The service to update</param>
    /// <param name="title">The new title of the service</param>
    /// <param name="description">The new description of the service</param>
    /// <param name="price">The new price of the service</param>
    /// <param name="availableBegin">The new starting availability time of the service</param>
    /// <param name="availableEnd">The new ending availability time of the service</param>
    /// <returns></returns>
    Task<Service> UpdateAsync(
        Service service,
        string title,
        string description,
        decimal price,
        DateTime availableBegin,
        DateTime availableEnd
    );

    /// <summary>
    /// Asynchrnously unlists a service
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    Task UnlistAsync(Service service);

    /// <summary>
    /// Asynchronoysly checks whether a service is available in the
    /// prodived date/time range
    /// </summary>
    /// <param name="service">The service to check</param>
    /// <returns></returns>
    Task<bool> IsAvailableAsync(Service service);

    /// <summary>
    /// Asynchronously changes the availability flag of a service
    /// </summary>
    /// <param name="service">The service to change</param>
    /// <param name="isAvailable">The new availability value</param>
    /// <returns>The updated service</returns>
    Task<Service> SetAvailability(Service service, bool isAvailable);

    /// <summary>
    /// Asynchronously creates a reservation for a service
    /// </summary>
    /// <param name="by">The user which to make the reservation for</param>
    /// <param name="service">The service to reserve</param>
    /// <returns>The service reservation</returns>
    Task<ServiceReservation> ReserveAsync(User by, Service service);
}