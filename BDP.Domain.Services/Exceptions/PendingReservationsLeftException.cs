using BDP.Domain.Entities;

namespace BDP.Domain.Services.Exceptions;

public sealed class PendingReservationsLeftException : Exception
{
    private readonly Service _service;

    /// <summary>
    /// Default constructor
    /// </summary>
    public PendingReservationsLeftException(Service service)
        : base($"service #{service.Id} still has pending reservations")
    {
        _service = service;
    }

    /// <summary>
    /// Gets the service associated with this exception
    /// </summary>
    public Service Service => _service;
}
