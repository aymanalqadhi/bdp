using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Domain.Services.Exceptions;

namespace BDP.Application.App;

/// <inheritdoc/>
public sealed class ProductsService : IProductsService
{
    #region Fields

    private readonly IAttachmentsService _attachmentsSvc;
    private readonly IPurchasesService _purchasesSvc;
    private readonly ITransactionsService _transactionsSvc;
    private readonly IUnitOfWork _uow;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the application</param>
    /// <param name="purchasesSvc">Purchase managment service</param>
    /// <param name="attachmentsSvc">Attachment managment service</param>
    public ProductsService(
        IUnitOfWork uow,
        IPurchasesService purchasesSvc,
        IAttachmentsService attachmentsSvc,
        ITransactionsService transactionsSvc)
    {
        _uow = uow;
        _purchasesSvc = purchasesSvc;
        _attachmentsSvc = attachmentsSvc;
        _transactionsSvc = transactionsSvc;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public async Task<Product> AddAsync(
        EntityKey<User> userId,
        string title,
        string description,
        IEnumerable<Category> categoryIds)
    {
        var user = await _uow.Users.Query().FindAsync(userId);
        var categories = _uow.Categories.Query().Where(c => categoryIds.Contains(c));

        var product = new Product
        {
            Categories = await categories.AsAsyncEnumerable().ToListAsync(),
            Title = title,
            Description = description,
            IsAvailable = true,
            OfferedBy = user,
        };

        _uow.Products.Add(product);
        await _uow.CommitAsync();

        return product;
    }

    /// <inheritdoc/>
    public Task<ProductVariant> AddReservableVariantAsync(
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

    /// <inheritdoc/>
    public Task<ProductVariant> AddSellableVariantAsync(
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
    public IQueryBuilder<Product> GetByCategory(EntityKey<Category> categoryId)
        => _uow.Products.Query().Where(p => p.Categories.Any(c => c.Id == categoryId));

    /// <inheritdoc/>
    public IQueryBuilder<Product> GetFor(EntityKey<User> userId)
        => _uow.Products.Query().Where(s => s.OfferedBy.Id == userId);

    /// <inheritdoc/>
    public IQueryBuilder<Product> GetProducts()
        => _uow.Products.Query();

    /// <inheritdoc/>
    public async Task RemoveAsync(EntityKey<Product> productId, bool cancelPurchases = false)
    {
        if (!cancelPurchases && await _purchasesSvc.HasPendingPurchasesAsync(productId))
            throw new PendingPurchasesLeftException(productId);

        var product = await _uow.Products.Query().FindAsync(productId);

        var transactions = await _purchasesSvc.PendingReservations(productId)
            .Select(r => r.Payment)
            .AsAsyncEnumerable()
            .ToListAsync();

        transactions.AddRange(await _purchasesSvc.PendingOrders(productId)
            .Select(o => o.Payment)
            .AsAsyncEnumerable()
            .ToListAsync());

        await Task.WhenAll(transactions.Select(t => _transactionsSvc.CancelAsync(t.To.Id, t.Id)));

        _uow.Products.Remove(product);
        await _uow.CommitAsync();
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
    public IQueryBuilder<Product> Search(string query)
        => _uow.Products.Query().Where(s => s.Title.Contains(query, StringComparison.OrdinalIgnoreCase));

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
        if (price <= 0 || price > 1_000_000)
            throw new InvalidPriceException(price);

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

    #endregion Private Methods
}