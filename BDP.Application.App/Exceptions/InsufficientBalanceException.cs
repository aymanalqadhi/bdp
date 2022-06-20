using BDP.Domain.Entities;

namespace BDP.Application.App.Exceptions;

public class InsufficientBalanceException : Exception
{
    #region Fields

    private readonly EntityKey<User> _userId;
    private readonly decimal _amount;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="from">The user which requsted the specified amount</param>
    /// <param name="amount">The amount requsted</param>
    public InsufficientBalanceException(EntityKey<User> userId, decimal amount)
        : base($"user `{userId}' does not have {amount} in balance")
    {
        _userId = userId;
        _amount = amount;
    }

    #endregion Public Constructors

    #region Properties

    /// <summary>
    /// Gets the requested amount
    /// </summary>
    public decimal Amount => _amount;

    /// <summary>
    /// Gets the user who lacks sufficient balance
    /// </summary>
    public EntityKey<User> UserId => _userId;

    #endregion Properties
}