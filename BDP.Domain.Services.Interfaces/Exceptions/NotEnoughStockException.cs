using BDP.Domain.Entities;

namespace BDP.Domain.Services.Exceptions;

/// <summary>
/// An exception to be thrown when a quantity that does not exist is requsted
/// from a sellable product variant
/// </summary>
public class NotEnoughStockException : Exception
{
    #region Fields

    private readonly uint _requestedQuantity;
    private readonly EntityKey<ProductVariant> _variantid;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="variantid">The product lacking stock</param>
    /// <param name="requestedQuantity">The requested quantity</param>
    public NotEnoughStockException(EntityKey<ProductVariant> variantid, uint requestedQuantity)
        : base($"product variant #{variantid.Id} does not have {requestedQuantity} stock left")
    {
        _variantid = variantid;
        _requestedQuantity = requestedQuantity;
    }

    #endregion Public Constructors

    #region Properties

    /// <summary>
    /// Gets the product associated with this exception
    /// </summary>
    public EntityKey<ProductVariant> ProductVariantId => _variantid;

    /// <summary>
    /// Gets the requested quantity
    /// </summary>
    public uint RequestedQuantity => _requestedQuantity;

    #endregion Properties
}