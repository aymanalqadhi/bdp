using BDP.Domain.Entities;

namespace BDP.Application.App.Exceptions;

public sealed class TransactionAlreadyConfirmedException : Exception
{
    private readonly TransactionConfirmation _confirmation;

    /// <summary>
    /// Default constructor
    /// </summary>
    public TransactionAlreadyConfirmedException(TransactionConfirmation confirmation)
        : base($"transaction has already been confirmed")
    {
        _confirmation = confirmation;
    }

    /// <summary>
    /// Gets the transaction associated with the error
    /// </summary>
    public TransactionConfirmation Confirmation => _confirmation;
}