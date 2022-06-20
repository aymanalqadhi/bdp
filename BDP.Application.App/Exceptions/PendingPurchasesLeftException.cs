using BDP.Domain.Entities;

namespace BDP.Application.App.Exceptions;

public sealed class PendingPurchasesLeftException : Exception
{
    #region Fields

    private readonly EntityKey<Product> _productId;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    public PendingPurchasesLeftException(EntityKey<Product> productId)
        : base($"product #{productId} still has pending purchases")
    {
        _productId = productId;
    }

    #endregion Public Constructors

    #region Properties

    /// <summary>
    /// Gets the id of the product which still has pending purchases
    /// </summary>
    public EntityKey<Product> ProductId => _productId;

    #endregion Properties
}