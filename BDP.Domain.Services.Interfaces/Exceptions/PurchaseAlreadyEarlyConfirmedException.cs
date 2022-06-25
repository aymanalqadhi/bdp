using BDP.Domain.Entities;

namespace BDP.Domain.Services.Exceptions;

/// <summary>
/// A base class for an exception that is thrown when a purchase is already early confirmed
/// </summary>
/// <typeparam name="TPurchase">The type of the purchase</typeparam>
public abstract class PurchaseAlreadyEarlyConfirmedException<TPurchase> : Exception
    where TPurchase : Purchase<TPurchase>
{
    #region Fields

    private readonly EntityKey<TPurchase> _purchaseId;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="purchaseId">The id of the already confirmed purchase</param>
    /// <param name="message">An optional message to calrify the error</param>
    public PurchaseAlreadyEarlyConfirmedException(
        EntityKey<TPurchase> purchaseId,
        string? message = null) : base(message)
    {
        _purchaseId = purchaseId;
    }

    #endregion Public Constructors

    #region Properties

    /// <summary>
    /// Gets the already confirmed purchase id
    /// </summary>
    public EntityKey<TPurchase> PurchaseId => _purchaseId;

    #endregion Properties
}