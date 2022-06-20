using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;

using System.Linq.Expressions;

namespace BDP.Application.App;

public class ProductsService : IProductsService
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
    public ProductsService(IUnitOfWork uow, IAttachmentsService attachmentsSvc, IFinanceService financeSvc)
    {
        _uow = uow;
        _attachmentsSvc = attachmentsSvc;
        _financeSvc = financeSvc;
    }

    /// <inheritdoc/>
    public Task<Product> GetByIdAsync(Guid id)
    {
        return _uow.Products
            .Query()
            .Include(p => p.OfferedBy)
            .Include(p => p.Attachments)
            .FindAsync(id);
    }

    /// <inheritdoc/>
    public async Task<Product> ListAsync(
        Guid userid,
        string title,
        string description,
        decimal price,
        uint quantity,
        IEnumerable<IUploadFile>? attachments = null)
    {
        if (price <= 0 || price > 1_000_000)
            throw new InvalidPriceException(price);

        if (quantity <= 0 || quantity > 10_000)
            throw new InvalidQuantityException(quantity);

        var user = await _uow.Users.Query().FindAsync(userid);

        var product = new Product
        {
            Title = title,
            Description = description,
            Price = price,
            Quantity = quantity,
            IsAvailable = true,
            OfferedBy = user,
        };

        if (attachments != null)
            product.Attachments = await _attachmentsSvc.SaveAllAsync(attachments).ToListAsync();

        _uow.Products.Add(product);
        await _uow.CommitAsync();

        return product;
    }

    /// <inheritdoc/>
    public async Task<Product> UpdateAsync(
        Guid productId,
        string title,
        string description,
        decimal price)
    {
        if (price <= 0 || price > 1_000_000)
            throw new InvalidPriceException(price);

        var product = await _uow.Products.Query().FindAsync(productId);

        product.Title = title;
        product.Description = description;
        product.Price = price;

        _uow.Products.Update(product);
        await _uow.CommitAsync();

        return product;
    }

    /// <inheritdoc/>
    public async Task UnlistAsync(Guid productId)
    {
        if (await _uow.ProductOrders.Query().AnyAsync(o =>
            o.Product.Id == productId &&
            o.Transaction.Confirmation == null))
        {
            throw new PendingOrdersLeftException(productId);
        }

        var product = await _uow.Products.Query().FindAsync(productId);
        _uow.Products.Remove(product);

        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task<long> AvailableQuantityAsync(Guid productId)
    {
        // TODO:
        // Use transactions here

        var product = await _uow.Products.Query().FindAsync(productId);
        var orderedQuantity = await _uow.ProductOrders.Query()
            .Where(o => o.Product.Id == productId)
            .Where(o => o.Transaction.Confirmation == null || o.Transaction.Confirmation.Outcome == TransactionConfirmationOutcome.Confirmed)
            .AsAsyncEnumerable()
            .SumAsync(o => o.Quantity);

        if (orderedQuantity > product.Quantity)
            throw new InvalidQuantityException(product.Quantity);

        return product.Quantity - orderedQuantity;
    }

    /// <inheritdoc/>
    public async Task<bool> IsAvailableAsync(Guid productId)
        => await AvailableQuantityAsync(productId) > 0;

    /// <inheritdoc/>
    public async Task<Product> SetAvailability(Guid productId, bool isAvailable)
    {
        var product = await _uow.Products.Query().FindAsync(productId);

        if (product.IsAvailable == isAvailable)
            return product;

        product.IsAvailable = isAvailable;

        _uow.Products.Update(product);
        await _uow.CommitAsync();

        return product;
    }

    /// <inheritdoc/>
    public async Task<ProductOrder> OrderAsync(Guid userId, Guid productId, uint quantity)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        var product = await _uow.Products.Query().FindAsync(productId);
        decimal totalPrice = product.Price * quantity;

        if (await AvailableQuantityAsync(productId) < quantity)
            throw new NotEnoughStockException(product, quantity);

        if (await _financeSvc.CalculateTotalUsableAsync(userId) < totalPrice)
            throw new InsufficientBalanceException(userId, totalPrice);

        var transaction = await _financeSvc.TransferUncomittedAsync(userId, product.OfferedBy.Id, totalPrice);
        var order = new ProductOrder
        {
            Product = product,
            Quantity = quantity,
            Transaction = transaction,
        };

        _uow.ProductOrders.Add(order);
        await _uow.CommitAsync(tx);

        return order;
    }
}