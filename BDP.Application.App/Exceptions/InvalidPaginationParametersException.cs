namespace BDP.Application.App.Exceptions;

public class InvalidPaginationParametersException : Exception
{
    private readonly int _page;
    private readonly int _pageSize;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="page">The requested page</param>
    /// <param name="pageSize">The requested page size</param>
    public InvalidPaginationParametersException(int page, int pageSize)
        : base($"invalid pagination parameters: page = {page}, pageSize = {pageSize}")
    {
        _page = page;
        _pageSize = pageSize;
    }

    /// <summary>
    /// Gets the requested page number
    /// </summary>
    public int Page => _page;

    /// <summary>
    /// Gets the requested page size
    /// </summary>
    public int PageSize => _pageSize;
}