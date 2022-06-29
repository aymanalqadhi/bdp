﻿using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Domain.Services.Exceptions;

namespace BDP.Application.App;

/// <summary>
/// A service to manage application purchases
/// </summary>
public class PurchasesService : IPurchasesService
{
    #region Fields

    private readonly IFinanceService _financeSvc;
    private readonly IStockBatchesService _stockSvc;
    private readonly IUnitOfWork _uow;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="financeSvc">The finance managment service</param>
    /// <param name="stockSvc">The stock batches managment service</param>
    /// <param name="uow">The unit of work of the app</param>
    public PurchasesService(
        IFinanceService financeSvc,
        IStockBatchesService stockSvc,
        IUnitOfWork uow)
    {
        _financeSvc = financeSvc;
        _stockSvc = stockSvc;
        _uow = uow;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public IQueryBuilder<Order> GetOrdersFor(EntityKey<Product> productId, bool pending = false)
    {
        var query = _uow.Orders.Query().Where(o => o.Variant.Product.Id == productId);

        if (pending)
            return query.Where(o => o.Payment.Confirmation == null);

        return query;
    }

    /// <inheritdoc/>
    public IQueryBuilder<Order> GetOrdersFor(EntityKey<User> userId, bool pending = false)
    {
        var query = _uow.Orders.Query()
            .Where(o => o.Payment.From.Id == userId || o.Payment.To.Id == userId);

        if (pending)
            return query.Where(o => o.Payment.Confirmation == null);

        return query;
    }

    /// <inheritdoc/>
    public IQueryBuilder<Reservation> GetReservationsFor(EntityKey<Product> productId, bool pending = false)
    {
        var query = _uow.Reservations.Query().Where(r => r.Variant.Product.Id == productId);

        if (pending)
            return query.Where(r => r.Payment.Confirmation == null);

        return query;
    }

    /// <inheritdoc/>
    public IQueryBuilder<Reservation> GetReservationsFor(EntityKey<User> userId, bool pending = false)
    {
        var query = _uow.Reservations.Query()
            .Where(r => r.Payment.From.Id == userId || r.Payment.To.Id == userId);

        if (pending)
            return query.Where(r => r.Payment.Confirmation == null);

        return query;
    }

    /// <inheritdoc/>
    public async Task<bool> HasPendingPurchasesAsync(EntityKey<Product> productId)
    {
        return await GetOrdersFor(productId, true).AnyAsync() ||
               await GetReservationsFor(productId, true).AnyAsync();
    }

    /// <inheritdoc/>
    public async Task<Order> OrderAsync(EntityKey<User> userId, EntityKey<ProductVariant> variantId, uint quantity)
    {
        var variant = await _uow.ProductVariants.Query()
            .Include(v => v.Product)
            .Include(v => v.Product.OwnedBy)
            .FindAsync(variantId);

        if (variant.Type is not ProductVariantType.Sellable)
            throw new InvalidProductVaraintTypeException(variantId, ProductVariantType.Sellable, variant.Type);

        await using var tx = await _uow.BeginTransactionAsync();

        var totalPrice = variant.Price * quantity;

        if (await _stockSvc.AvailableQuantityAsync(variantId) < quantity)
            throw new NotEnoughStockException(variantId, quantity);

        if (await _financeSvc.CalculateBalanceAsync(userId) < totalPrice)
            throw new InsufficientBalanceException(userId, totalPrice);

        var transaction = await _financeSvc.TransferUncomittedAsync(
            userId, variant.Product.OwnedBy.Id, totalPrice);

        var order = new Order
        {
            Variant = variant,
            Quantity = quantity,
            Payment = transaction,
            IsEarlyAccepted = false,
        };

        _uow.Orders.Add(order);
        await _uow.CommitAsync(tx);

        return order;
    }

    /// <inheritdoc/>
    public async Task<Reservation> ReserveAsync(EntityKey<User> userId, EntityKey<ProductVariant> variantId)
    {
        var variant = await _uow.ProductVariants.Query()
            .Include(v => v.Product)
            .Include(v => v.Product.OwnedBy)
            .FindAsync(variantId);

        if (variant.Type is not ProductVariantType.Reservable)
            throw new InvalidProductVaraintTypeException(variantId, ProductVariantType.Reservable, variant.Type);

        var user = await _uow.Users.Query().FindAsync(userId);

        await using var tx = await _uow.BeginTransactionAsync();

        if (await _financeSvc.CalculateBalanceAsync(userId) < variant.Price)
            throw new InsufficientBalanceException(userId, variant.Price);

        var transaction = await _financeSvc.TransferUncomittedAsync(userId, variant.Product.OwnedBy.Id, variant.Price);
        var reservation = new Reservation
        {
            Variant = variant,
            Payment = transaction,
            IsEarlyAccepted = false,
        };

        _uow.Reservations.Add(reservation);
        await _uow.CommitAsync(tx);

        return reservation;
    }

    /// <inheritdoc/>
    public async Task<Order> EarlyOrderConfirmAsync(
        EntityKey<User> userId,
        EntityKey<Order> orderId,
        bool isAccepted)
    {
        var order = await _uow.Orders.Query()
            .Include(o => o.Payment)
            .Include(o => o.Payment.To)
            .FindWithOwnershipValidationAsync(userId, orderId, o => o.Payment.To);

        if (order.IsEarlyAccepted is not null)
            throw new OrderAlreadyEarlyConfirmedException(orderId);

        order.IsEarlyAccepted = isAccepted;

        _uow.Orders.Update(order);
        await _uow.CommitAsync();

        return order;
    }

    /// <inheritdoc/>
    public async Task<Reservation> EarlyResrvationConfirmAsync(
        EntityKey<User> userId,
        EntityKey<Reservation> reservationId,
        bool isAccepted)
    {
        var reservation = await _uow.Reservations.Query()
            .Include(o => o.Payment)
            .Include(o => o.Payment.To)
            .FindWithOwnershipValidationAsync(userId, reservationId, o => o.Payment.To);

        if (reservation.IsEarlyAccepted is not null)
            throw new ReservationAlreadyEarlyConfirmedException(reservationId);

        reservation.IsEarlyAccepted = isAccepted;

        _uow.Reservations.Update(reservation);
        await _uow.CommitAsync();

        return reservation;
    }

    #endregion Public Methods
}