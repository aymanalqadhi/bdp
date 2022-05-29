namespace BDP.Domain.Services.Exceptions;

public class ItemAlreadyReviewedException : Exception
{
    public ItemAlreadyReviewedException(string message) : base(message)
    {
    }
}
