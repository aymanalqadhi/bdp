﻿using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Domain.Services.Exceptions;

namespace BDP.Application.App;

/// <summary>
/// A service to manage reservable product variants reservation windows
/// </summary>
public sealed class ReservationWindowsService : IReservationWindowsService
{
    #region Fields

    private readonly IUnitOfWork _uow;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the application</param>
    public ReservationWindowsService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public async Task<ReservationWindow> AddAsync(
        EntityKey<User> userId,
        EntityKey<ProductVariant> variantId,
        Weekday weekdays,
        TimeOnly start,
        TimeOnly end)
    {
        var variant = await _uow.ProductVariants.Query()
            .Include(v => v.Product)
            .Include(v => v.Product.OfferedBy)
            .FindWithOwnershipValidationAsync(userId, variantId, v => v.Product.OfferedBy);

        if (variant.Type != ProductVariantType.Reservable)
            throw new InvalidProductVaraintTypeException(variantId, ProductVariantType.Reservable, variant.Type);

        var window = new ReservationWindow
        {
            AvailableDays = weekdays,
            Start = start,
            End = end,
        };

        _uow.ReservationWindows.Add(window);
        await _uow.CommitAsync();

        return window;
    }

    /// <inheritdoc/>
    public async Task RemoveAsync(EntityKey<User> userId, EntityKey<ReservationWindow> windowId)
    {
        var window = await _uow.ReservationWindows.Query()
            .Include(b => b.Variant)
            .Include(b => b.Variant.Product)
            .Include(b => b.Variant.Product.OfferedBy)
            .FindWithOwnershipValidationAsync(userId, windowId, b => b.Variant.Product.OfferedBy);

        _uow.ReservationWindows.Remove(window);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public IQueryBuilder<ReservationWindow> GetReservationWindows(EntityKey<ProductVariant> variantId)
        => _uow.ReservationWindows.Query().Where(b => b.Variant.Id == variantId);

    #endregion Public Methods
}