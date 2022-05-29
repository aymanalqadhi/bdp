namespace BDP.Domain.Services.Exceptions;

public class InvalidDepositAmountException : Exception
{
    public InvalidDepositAmountException(decimal amount)
        : base($"invalid deposit amount {amount}")
    {
    }
}
