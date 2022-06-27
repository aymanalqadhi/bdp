using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Domain.Services.Exceptions;

namespace BDP.Application.App;

/// <summary>
/// A service to manage sellable product variants stock batches
/// </summary>
public sealed class StockBatchesService : IStockBatchesService
{
    #region Fields

    private readonly IUnitOfWork _uow;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the application</param>
    public StockBatchesService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public async Task<StockBatch> AddAsync(EntityKey<User> userId, EntityKey<ProductVariant> variantId, uint quantity)
    {
        var variant = await _uow.ProductVariants.Query()
            .Include(v => v.Product)
            .Include(v => v.Product.OfferedBy)
            .FindWithOwnershipValidationAsync(userId, variantId, v => v.Product.OfferedBy);

        if (variant.Type != ProductVariantType.Sellable)
            throw new InvalidProductVaraintTypeException(variantId, ProductVariantType.Sellable, variant.Type);

        var batch = new StockBatch
        {
            Quantity = quantity,
            Variant = variant,
        };

        _uow.StockBatches.Add(batch);
        await _uow.CommitAsync();

        return batch;
    }

    /// <inheritdoc/>
    public IQueryBuilder<StockBatch> GetStockBatches(EntityKey<ProductVariant> variantId)
        => _uow.StockBatches.Query().Where(b => b.Variant.Id == variantId);

    /// <inheritdoc/>
    public async Task RemoveAsync(EntityKey<User> userId, EntityKey<StockBatch> batchId)
    {
        var batch = await _uow.StockBatches.Query()
            .Include(b => b.Variant)
            .Include(b => b.Variant.Product)
            .Include(b => b.Variant.Product.OfferedBy)
            .FindWithOwnershipValidationAsync(userId, batchId, b => b.Variant.Product.OfferedBy);

        await using var tx = await _uow.BeginTransactionAsync();

        if (await AvailableQuantityAsync(batch.Variant.Id) < batch.Quantity)
            throw new NotEnoughStockException(batch.Variant.Id, batch.Quantity);

        _uow.StockBatches.Remove(batch);
        await _uow.CommitAsync(tx);
    }

    /// <inheritdoc/>
    public async Task<long> AvailableQuantityAsync(EntityKey<ProductVariant> variantId)
    {
        var availableQuantity = await _uow.StockBatches.Query()
            .Where(b => b.Variant.Id == variantId)
            .AsAsyncEnumerable()
            .SumAsync(b => b.Quantity);

        var totalOrderedQuantity = await _uow.Orders.Query()
            .Where(o => o.Variant.Id == variantId)
            .Where(o => o.Payment.Confirmation == null || o.Payment.Confirmation.IsAccepted)
            .AsAsyncEnumerable()
            .SumAsync(o => o.Quantity);

        return availableQuantity - totalOrderedQuantity;
    }

    #endregion Public Methods
}