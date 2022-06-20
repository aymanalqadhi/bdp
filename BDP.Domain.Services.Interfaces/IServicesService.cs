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
    /// <param name="userId">The id of the user to list the service for</param>
    /// <param name="title">The title of the product</param>
    /// <param name="description">The description of the product</param>
    /// <param name="price">The price of the product</param>
    /// <param name="availableBegin">The starting availability time of the service</param>
    /// <param name="availableEnd">The ending availability time of the service</param>
    /// <param name="attachments">The attachments of the service</param>
    /// <returns>The listed service</returns>
    Task<Service> ListAsync(
        Guid userId,
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
    /// <param name="serviceId">The id of the service to update</param>
    /// <param name="title">The new title of the service</param>
    /// <param name="description">The new description of the service</param>
    /// <param name="price">The new price of the service</param>
    /// <param name="availableBegin">The new starting availability time of the service</param>
    /// <param name="availableEnd">The new ending availability time of the service</param>
    /// <returns></returns>
    Task<Service> UpdateAsync(
        Guid serviceId,
        string title,
        string description,
        decimal price,
        DateTime availableBegin,
        DateTime availableEnd
    );

    /// <summary>
    /// Asynchrnously unlists a service
    /// </summary>
    /// <param name="serviceId">The id of the service to unlist</param>
    /// <returns></returns>
    Task UnlistAsync(Guid serviceId);

    /// <summary>
    /// Asynchronously changes the availability flag of a service
    /// </summary>
    /// <param name="serviceId">The id of the service to change</param>
    /// <param name="isAvailable">The new availability value</param>
    /// <returns>The updated service</returns>
    Task<Service> SetAvailability(Guid serviceId, bool isAvailable);

    /// <summary>
    /// Asynchronously creates a reservation for a service
    /// </summary>
    /// <param name="userId">The id of the user which to make the reservation for</param>
    /// <param name="serviceId">The id of the service to reserve</param>
    /// <returns>The service reservation</returns>
    Task<ServiceReservation> ReserveAsync(Guid userId, Guid serviceId);
}