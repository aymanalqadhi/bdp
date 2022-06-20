using BDP.Domain.Entities;

namespace BDP.Domain.Services.Exceptions;

public class InsufficientBalanceException : Exception
{
    private readonly User _from;
    private readonly decimal _amount;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="from">The user which requsted the specified amount</param>
    /// <param name="amount">The amount requsted</param>
    public InsufficientBalanceException(User from, decimal amount)
        : base($"user {from.Username} does not have {amount} in balance")
    {
        _from = from;
        _amount = amount;
    }

    /// <summary>
    /// Gets the user who lacks sufficient balance
    /// </summary>
    public User From => _from;

    /// <summary>
    /// Gets the requested amount
    /// </summary>
    public decimal Amount => _amount;
}
