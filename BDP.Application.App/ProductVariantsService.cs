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
        EntityKey<User> userId,
        EntityKey<Product> productId,
        string name,
        string? description,
        decimal price,
        IEnumerable<IUploadFile>? attachments = null)
    {
        return AddVariantAsync(
            userId,
            productId,
            ProductVariantType.Reservable,
            name,
            description,
            price,
            attachments);
    }

    /// <inheritdoc/>
    public Task<ProductVariant> AddSellableAsync(
        EntityKey<User> userId,
        EntityKey<Product> productId,
        string name,
        string? description,
        decimal price,
        IEnumerable<IUploadFile>? attachments = null)
    {
        return AddVariantAsync(
            userId,
            productId,
            ProductVariantType.Reservable,
            name,
            description,
            price,
            attachments);
    }

    /// <inheritdoc/>
    public IQueryBuilder<ProductVariant> GetVariants(EntityKey<Product> productId)
        => _uow.ProductVariants.Query().Where(v => v.Product.Id == productId);

    /// <inheritdoc/>
    public async Task RemoveVariantAsync(EntityKey<User> userId, EntityKey<ProductVariant> variantid)
    {
        var variant = await _uow.ProductVariants.Query()
            .Include(v => v.Product)
            .Include(v => v.Product.OwnedBy)
            .FindWithOwnershipValidationAsync(userId, variantid, v => v.Product.OwnedBy);

        _uow.ProductVariants.Remove(variant);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task<ProductVariant> UpdateAsync(
        EntityKey<User> userId,
        EntityKey<ProductVariant> variantId,
        string name,
        string? description,
        decimal price)
    {
        InvalidPriceException.ValidatePrice(price);

        var variant = await _uow.ProductVariants.Query()
            .Include(v => v.Product)
            .Include(v => v.Product.OwnedBy)
            .FindWithOwnershipValidationAsync(userId, variantId, v => v.Product.OwnedBy);

        variant.Name = name;
        variant.Description = description;
        variant.Price = price;

        _uow.ProductVariants.Update(variant);
        await _uow.CommitAsync();

        return variant;
    }

    #endregion Public Methods

    #region Private Methods

    private async Task<ProductVariant> AddVariantAsync(
        EntityKey<User> userId,
        EntityKey<Product> productId,
        ProductVariantType type,
        string name,
        string? description,
        decimal price,
        IEnumerable<IUploadFile>? attachments = null)
    {
        InvalidPriceException.ValidatePrice(price);

        var product = await _uow.Products.Query()
            .FindWithOwnershipValidationAsync(userId, productId);

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