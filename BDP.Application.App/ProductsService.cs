using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
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
    public async Task<Product> GetByIdAsync(long id)
    {
        var product = await _uow.Products.GetAsync(
            id,
            includes: new Expression<Func<Product, object>>[]
            {
                p => p.OfferedBy, p => p.Attachments
            }
       );

        if (product is null)
            throw new NotFoundException($"no products were found with id #{id}");

        return product;
    }

    /// <inheritdoc/>
    public async Task<Product> ListAsync(
        User user,
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
        Product product,
        string title,
        string description,
        decimal price)
    {
        if (price <= 0 || price > 1_000_000)
            throw new InvalidPriceException(price);

        product.Title = title;
        product.Description = description;
        product.Price = price;

        _uow.Products.Update(product);
        await _uow.CommitAsync();

        return product;
    }

    /// <inheritdoc/>
    public async Task UnlistAsync(Product product)
    {
        if (await _uow.ProductOrders.AnyAsync(
            o => o.Product.Id == product.Id && o.Transaction.Confirmation == null))
            throw new PendingOrdersLeftException(product);

        _uow.Products.Remove(product);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task<Product> AddStock(Product product, uint quantity)
    {
        product.Quantity += quantity;

        _uow.Products.Update(product);
        await _uow.CommitAsync();

        return product;
    }

    /// <inheritdoc/>
    public async Task<Product> RemoveStock(Product product, uint quantity)
    {
        if (quantity > await AvailableQuantityAsync(product))
            throw new InvalidQuantityException(product.Quantity);

        product.Quantity -= quantity;
        await _uow.CommitAsync();

        return product;
    }

    /// <inheritdoc/>
    public async Task<long> AvailableQuantityAsync(Product product)
    {
        var orderedQuantity = await _uow.ProductOrders.FilterAsync(o =>
            o.Product.Id == product.Id &&
            (o.Transaction.Confirmation == null ||
             o.Transaction.Confirmation.Outcome == TransactionConfirmationOutcome.Confirmed)
        ).SumAsync(o => o.Quantity);

        if (orderedQuantity > product.Quantity)
            throw new InvalidQuantityException(product.Quantity);

        return product.Quantity - orderedQuantity;
    }

    /// <inheritdoc/>
    public async Task<bool> IsAvailableAsync(Product product)
        => product.IsAvailable && await AvailableQuantityAsync(product) > 0;

    /// <inheritdoc/>
    public async Task<Product> SetAvailability(Product product, bool isAvailable)
    {
        if (product.IsAvailable == isAvailable)
            return product;

        product.IsAvailable = isAvailable;

        _uow.Products.Update(product);
        await _uow.CommitAsync();

        return product;
    }

    /// <inheritdoc/>
    public async Task<ProductOrder> OrderAsync(User by, Product product, uint quantity)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        decimal totalPrice = product.Price * quantity;

        if (await AvailableQuantityAsync(product) < quantity)
            throw new NotEnoughStockException(product, quantity);

        if (await _financeSvc.CalculateTotalUsableAsync(by) < totalPrice)
            throw new InsufficientBalanceException(by, totalPrice);

        var transaction = await _financeSvc.TransferUncomittedAsync(by, product.OfferedBy, totalPrice);
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