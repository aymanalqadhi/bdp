using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Domain.Services.Exceptions;

using System.Linq.Expressions;

namespace BDP.Application.App;

public class PurchasesService : IPurchasesService
{
    #region Fields

    private readonly IFinanceService _financeSvc;
    private readonly IProductsService _productsSvc;
    private readonly IUnitOfWork _uow;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the app</param>
    /// <param name="productsSvc">The products service of the application</param>
    /// <param name="financeSvc"></param>
    public PurchasesService(IUnitOfWork uow, IProductsService productsSvc, IFinanceService financeSvc)
    {
        _uow = uow;
        _productsSvc = productsSvc;
        _financeSvc = financeSvc;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public IQueryBuilder<Order> GetOrders(EntityKey<Product> productId)
        => _uow.Orders.Query().Where(o => o.Variant.Product.Id == productId);

    /// <inheritdoc/>
    public IQueryBuilder<Reservation> GetReservations(EntityKey<Product> productId)
        => _uow.Reservations.Query().Where(o => o.Variant.Product.Id == productId);

    /// <inheritdoc/>
    public async Task<bool> HasPendingPurchasesAsync(EntityKey<Product> productId)
    {
        return await PendingOrders(productId).AnyAsync() ||
               await PendingReservations(productId).AnyAsync();
    }

    /// <inheritdoc/>
    public async Task<Order> OrderAsync(EntityKey<User> userId, EntityKey<ProductVariant> variantId, uint quantity)
    {
        var variant = await _uow.ProductVariants.Query()
            .Include(v => v.Product)
            .Include(v => v.Product.OfferedBy)
            .FindAsync(variantId);

        if (variant.Type is not ProductVariantType.Sellable)
            throw new InvalidProductVaraintTypeException(variantId, ProductVariantType.Sellable, variant.Type);

        await using var tx = await _uow.BeginTransactionAsync();

        var totalPrice = variant.Price * quantity;

        if (await _productsSvc.TotalAvailableQuantityAsync(variantId) < quantity)
            throw new NotEnoughStockException(variantId, quantity);

        if (await _financeSvc.CalculateTotalUsableAsync(userId) < totalPrice)
            throw new InsufficientBalanceException(userId, totalPrice);

        var transaction = await _financeSvc.TransferUncomittedAsync(
            userId, variant.Product.OfferedBy.Id, totalPrice);

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
    public IQueryBuilder<Order> PendingOrders(EntityKey<Product> productId)
        => GetOrders(productId).Where(r => r.Payment.Confirmation == null);

    /// <inheritdoc/>
    public IQueryBuilder<Reservation> PendingReservations(EntityKey<Product> productId)
        => GetReservations(productId).Where(r => r.Payment.Confirmation == null);

    /// <inheritdoc/>
    public async Task<Reservation> ReserveAsync(EntityKey<User> userId, EntityKey<ProductVariant> variantId)
    {
        var variant = await _uow.ProductVariants.Query()
            .Include(v => v.Product)
            .Include(v => v.Product.OfferedBy)
            .FindAsync(variantId);

        if (variant.Type is not ProductVariantType.Reservable)
            throw new InvalidProductVaraintTypeException(variantId, ProductVariantType.Reservable, variant.Type);

        var user = await _uow.Users.Query().FindAsync(userId);

        await using var tx = await _uow.BeginTransactionAsync();

        if (await _financeSvc.CalculateTotalUsableAsync(userId) < variant.Price)
            throw new InsufficientBalanceException(userId, variant.Price);

        var transaction = await _financeSvc.TransferUncomittedAsync(userId, variant.Product.OfferedBy.Id, variant.Price);
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

    #endregion Public Methods
}