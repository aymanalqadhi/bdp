namespace BDP.Domain.Services;

public interface ISearchSuggestionService
{
    /// <summary>
    /// Asynchronsously fetches suggestion for a query
    /// </summary>
    /// <param name="query">The query to search for</param>
    /// <param name="includeUsers">Whether to include user values or not</param>
    /// <param name="includeSellables">Whether to include sellable values or not</param>
    /// <returns></returns>
    Task<IEnumerable<SearchSuggestion>> FindSuggestionsAsync(
        string query,
        int length = 8,
        bool includeUsers = true,
        bool includeSellables = true
    );
}

public class SearchSuggestion
{
    /// <summary>
    /// Gets or sets the id that the suggestion id
    /// </summary>
    public Guid ItemId { get; set; }

    /// <summary>
    /// Gets or sets the title of the suggestion
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the type of the suggestion
    /// </summary>
    public string Type { get; set; } = null!;
}