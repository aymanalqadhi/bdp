using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services.Exceptions;
using BDP.Domain.Services.Interfaces;

namespace BDP.Domain.Services;

public class SearchSuggestionsService : ISearchSuggestionsService
{
    #region Private fields

    private readonly IUsersService _usersSvc;
    private readonly ISellablesService _sellablesSvc;

    #endregion

    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    public SearchSuggestionsService(IUsersService usersSvc, ISellablesService sellablesSvc)
    {
        _usersSvc = usersSvc;
        _sellablesSvc = sellablesSvc;
    }

    #endregion

    /// <inheritdoc/>
    public async Task<IEnumerable<SearchSuggestion>> FindSuggestionsAsync(
        string query,
        int length = 8,
        bool includeUsers = true,
        bool includeSellables = true)
    {
        if (length <= 0 || length > 100)
            throw new InvalidPaginationParametersException(1, length);

        var ret = new List<SearchSuggestion>();

        if (includeUsers)
        {
            var items = (await _usersSvc
                .SearchAsync(query, 1, length)
                .ToListAsync())
                .Select(u => new SearchSuggestion
                {
                    ItemId = u.Id,
                    Title = u.FullName ?? u.Username,
                    Type = $"user - @{u.Username}"
                });

            ret.AddRange(items);
        }

        if (includeSellables)
        {
            var items = (await _sellablesSvc
                .SearchAsync(query, 1, length)
                .ToListAsync())
                .Select(s => new SearchSuggestion
                {
                    ItemId = s.Id,
                    Title = s.Title,
                    Type = s is Service ? "service" : "product"
                });

            ret.AddRange(items);
        }

        ret.Sort((x, y) => x.Title.Length.CompareTo(y.Title.Length));

        return ret.Take(length);
    }
}
