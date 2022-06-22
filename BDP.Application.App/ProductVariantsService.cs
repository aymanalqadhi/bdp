using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Domain.Services.Exceptions;

namespace BDP.Application.App;

/// <summary>
/// A service to manage product variants
/// </summary>
public sealed class ProductVariantsService : IProductVariantsService
{
    #region Fields

    private readonly IUnitOfWork _uow;
    private readonly IAttachmentsService _attachmentsSvc;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the application</param>
    public ProductVariantsService(IUnitOfWork uow, IAttachmentsService attachmentsSvc)
    {
        _uow = uow;
        _attachmentsSvc = attachmentsSvc;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public Task<ProductVariant> AddReservableAsync(
        EntityKey<Product> productId,
        string name,
        string? description,
        decimal price,
        IEnumerable<IUploadFile>? attachments = null)
    {
        return AddVariantAsync(
            productId,
            ProductVariantType.Reservable,
            name,
            description,
            price,
            attachments);
    }

    /// <inheritdoc/>
    public Task<ProductVariant> AddSellableAsync(
        EntityKey<Product> productId,
        string name,
        string? description,
        decimal price,
        IEnumerable<IUploadFile>? attachments = null)
    {
        return AddVariantAsync(
            productId,
            ProductVariantType.Reservable,
            name,
            description,
            price,
            attachments);
    }

    /// <inheritdoc/>
    public async Task RemoveVariantAsync(EntityKey<ProductVariant> variantid)
    {
        var variant = await _uow.ProductVariants.Query().FindAsync(variantid);

        _uow.ProductVariants.Remove(variant);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public IQueryBuilder<ReservationWindow> ReservationWindowsFor(EntityKey<ProductVariant> variantId)
        => _uow.ReservationWindows.Query().Where(w => w.Variant.Id == variantId);

    /// <inheritdoc/>
    public IQueryBuilder<ProductVariant> GetVariants(EntityKey<Product> productId)
        => _uow.ProductVariants.Query().Where(v => v.Product.Id == productId);

    /// <inheritdoc/>
    public async Task<long> TotalAvailableQuantityAsync(EntityKey<ProductVariant> variantId)
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

    /// <inheritdoc/>
    public async Task RemoveReservationWindowAsync(EntityKey<ReservationWindow> windowId)
    {
        var window = await _uow.ReservationWindows.Query().FindAsync(windowId);

        _uow.ReservationWindows.Remove(window);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task RemoveStockBatchAsync(EntityKey<StockBatch> batchId)
    {
        var batch = await _uow.StockBatches.Query()
            .Include(b => b.Variant)
            .FindAsync(batchId);

        await using var tx = await _uow.BeginTransactionAsync();

        if (await TotalAvailableQuantityAsync(batch.Variant.Id) < batch.Quantity)
            throw new NotEnoughStockException(batch.Variant.Id, batch.Quantity);

        _uow.StockBatches.Remove(batch);
        await _uow.CommitAsync(tx);
    }

    /// <inheritdoc/>
    public async Task<StockBatch> AddStockBatchAsync(EntityKey<ProductVariant> variantId, uint quantity)
    {
        var variant = await _uow.ProductVariants.Query().FindAsync(variantId);

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
    public async Task<ReservationWindow> AddReservationWindowAsync(
        EntityKey<ProductVariant> variantId,
        Weekday weekdays,
        TimeOnly start,
        TimeOnly end)
    {
        var variant = await _uow.ProductVariants.Query().FindAsync(variantId);

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

    #endregion Public Methods

    #region Private Methods

    private async Task<ProductVariant> AddVariantAsync(
        EntityKey<Product> productId,
        ProductVariantType type,
        string name,
        string? description,
        decimal price,
        IEnumerable<IUploadFile>? attachments = null)
    {
        InvalidPriceException.ValidatePrice(price);

        var product = await _uow.Products.Query().Include(p => p.OfferedBy).FindAsync(productId);
        var variant = new ProductVariant
        {
            Name = name,
            Description = description,
            Price = price,
            Type = type,
            Product = product,
        };

        if (attachments is not null)
            variant.Attachments = await _attachmentsSvc.SaveAllAsync(attachments).ToListAsync();

        _uow.ProductVariants.Add(variant);
        await _uow.CommitAsync();

        return variant;
    }

    /// <inheritdoc/>
    public async Task<ProductVariant> UpdateAsync(EntityKey<ProductVariant> variantId, string name, string? description, decimal price)
    {
        InvalidPriceException.ValidatePrice(price);

        var variant = await _uow.ProductVariants.Query().FindAsync(variantId);

        variant.Name = name;
        variant.Description = description;
        variant.Price = price;

        _uow.ProductVariants.Update(variant);
        await _uow.CommitAsync();

        return variant;
    }

    #endregion Private Methods
}