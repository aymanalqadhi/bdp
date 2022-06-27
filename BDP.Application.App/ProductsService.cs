using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Domain.Services.Exceptions;

namespace BDP.Application.App;

/// <summary>
/// A service to manage products
/// </summary>
public sealed class ProductsService : IProductsService
{
    #region Fields

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
    /// <param name="transactionsSvc">Transactions service</param>
    public ProductsService(
        IUnitOfWork uow,
        IPurchasesService purchasesSvc,
        ITransactionsService transactionsSvc)
    {
        _uow = uow;
        _purchasesSvc = purchasesSvc;
        _transactionsSvc = transactionsSvc;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public async Task<Product> AddAsync(
        EntityKey<User> userId,
        string title,
        string description,
        IEnumerable<EntityKey<Category>> categoryIds)
    {
        var user = await _uow.Users.Query().FindAsync(userId);
        var categories = _uow.Categories.Query().Where(c => categoryIds.Contains(c.Id));

        var product = new Product
        {
            Categories = await categories.AsAsyncEnumerable().ToListAsync(),
            Title = title,
            Description = description,
            IsAvailable = true,
            OwnedBy = user,
        };

        _uow.Products.Add(product);
        await _uow.CommitAsync();

        return product;
    }

    /// <inheritdoc/>
    public IQueryBuilder<Product> GetByCategory(EntityKey<Category> categoryId)
        => _uow.Products.Query().Where(p => p.Categories.Any(c => c.Id == categoryId));

    /// <inheritdoc/>
    public IQueryBuilder<Product> GetFor(EntityKey<User> userId)
        => _uow.Products.Query().Where(s => s.OwnedBy.Id == userId);

    /// <inheritdoc/>
    public IQueryBuilder<Product> GetProducts()
        => _uow.Products.Query();

    /// <inheritdoc/>
    public async Task RemoveAsync(
        EntityKey<User> userId,
        EntityKey<Product> productId,
        bool cancelPurchases = false)
    {
        var product = await _uow.Products.Query()
            .FindWithOwnershipValidationAsync(userId, productId);

        if (await _purchasesSvc.HasPendingPurchasesAsync(productId))
        {
            if (!cancelPurchases)
                throw new PendingPurchasesLeftException(productId);

            var transactions = await _purchasesSvc.GetReservationsFor(productId, true)
                .Select(r => r.Payment)
                .AsAsyncEnumerable()
                .ToListAsync();

            transactions.AddRange(await _purchasesSvc.GetOrdersFor(productId, true)
                .Select(o => o.Payment)
                .AsAsyncEnumerable()
                .ToListAsync());

            await Task.WhenAll(transactions.Select(t => _transactionsSvc.CancelAsync(t.To.Id, t.Id)));
        }

        _uow.Products.Remove(product);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public IQueryBuilder<Product> Search(string query)
        => _uow.Products.Query()
            .Where(s => s.Title.Contains(query, StringComparison.OrdinalIgnoreCase));

    /// <inheritdoc/>
    public IQueryBuilder<Product> Search(string query, EntityKey<User> userId)
        => _uow.Products.Query()
            .Where(p => p.OwnedBy.Id == userId)
            .Where(s => s.Title.Contains(query, StringComparison.OrdinalIgnoreCase));

    /// <inheritdoc/>
    public async Task<Product> UpdateAsync(
        EntityKey<User> userId,
        EntityKey<Product> productId,
        string title,
        string description)
    {
        var product = await _uow.Products.Query()
            .FindWithOwnershipValidationAsync(userId, productId);

        product.Title = title;
        product.Description = description;

        _uow.Products.Update(product);
        await _uow.CommitAsync();

        return product;
    }

    #endregion Public Methods
}