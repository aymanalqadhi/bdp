using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services;
using System.Linq.Expressions;

namespace BDP.Application.App;

public class ServicesService : IServicesService
{
    private readonly IUnitOfWork _uow;
    private readonly IAttachmentsService _attachmentsSvc;
    private readonly IFinanceService _financeSvc;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the app</param>
    /// <param name="attachmentsSvc">The attachments managment service</param>
    /// <param name="financeSvc">The finance service</param>
    public ServicesService(IUnitOfWork uow, IAttachmentsService attachmentsSvc, IFinanceService financeSvc)
    {
        _uow = uow;
        _attachmentsSvc = attachmentsSvc;
        _financeSvc = financeSvc;
    }

    /// <inheritdoc/>
    public async Task<Service> GetByIdAsync(long id)
    {
        var service = await _uow.Services
            .Query()
            .Include(s => s.Attachments)
            .Include(s => s.OfferedBy)
            .FindOrDefaultAsync(id);

        if (service is null)
            throw new NotFoundException($"no services were found with id #{id}");

        return service;
    }

    /// <inheritdoc/>
    public async Task<Service> ListAsync(
        User user,
        string title,
        string description,
        decimal price,
        DateTime availableBegin,
        DateTime availableEnd,
        IEnumerable<IUploadFile>? attachments = null)
    {
        if (price <= 0 || price > 1_000_000)
            throw new InvalidPriceException(price);

        var service = new Service
        {
            Title = title,
            Description = description,
            Price = price,
            AvailableBegin = availableBegin,
            AvailableEnd = availableEnd,
            IsAvailable = true,
            OfferedBy = user,
        };

        if (attachments != null)
            service.Attachments = await _attachmentsSvc.SaveAllAsync(attachments).ToListAsync();

        _uow.Services.Add(service);
        await _uow.CommitAsync();

        return service;
    }

    /// <inheritdoc/>
    public async Task<Service> UpdateAsync(
        Service service,
        string title,
        string description,
        decimal price,
        DateTime availableBegin,
        DateTime availableEnd)
    {
        if (price <= 0 || price > 1_000_000)
            throw new InvalidPriceException(price);

        service.Title = title;
        service.Description = description;
        service.Price = price;
        service.AvailableBegin = availableBegin;
        service.AvailableEnd = availableEnd;

        _uow.Services.Update(service);
        await _uow.CommitAsync();

        return service;
    }

    /// <inheritdoc/>
    public async Task UnlistAsync(Service service)
    {
        if (await _uow.ServiceReservations.Query().AnyAsync(
            o => o.Service.Id == service.Id && o.Transaction.Confirmation == null))
            throw new PendingReservationsLeftException(service);

        _uow.Services.Remove(service);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public Task<bool> IsAvailableAsync(Service service)
        => Task.FromResult(service.IsAvailable);

    /// <inheritdoc/>
    public async Task<Service> SetAvailability(Service service, bool isAvailable)
    {
        if (service.IsAvailable == isAvailable)
            return service;

        service.IsAvailable = isAvailable;

        _uow.Services.Update(service);
        await _uow.CommitAsync();

        return service;
    }

    /// <inheritdoc/>
    public async Task<ServiceReservation> ReserveAsync(User by, Service service)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        if (await _financeSvc.CalculateTotalUsableAsync(by) < service.Price)
            throw new InsufficientBalanceException(by, service.Price);

        var transaction = await _financeSvc.TransferUncomittedAsync(by, service.OfferedBy, service.Price);
        var reservation = new ServiceReservation
        {
            Service = service,
            Transaction = transaction,
        };

        _uow.ServiceReservations.Add(reservation);
        await _uow.CommitAsync(tx);

        return reservation;
    }
}