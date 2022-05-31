namespace BDP.Application.App.Exceptions;

public class ItemAlreadyReviewedException : Exception
{
    public ItemAlreadyReviewedException(string message) : base(message)
    {
    }
}
