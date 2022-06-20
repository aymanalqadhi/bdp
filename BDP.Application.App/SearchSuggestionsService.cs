using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;

namespace BDP.Application.App;

public class SearchSuggestionsService : ISearchSuggestionsService
{
    #region Private fields

    private readonly IUserProfilesService _userProfilesSvc;
    private readonly IProductsService _productsSvc;

    #endregion Private fields

    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    public SearchSuggestionsService(
        IUserProfilesService userProfilesSvc,
        IProductsService productsSvc)
    {
        _userProfilesSvc = userProfilesSvc;
        _productsSvc = productsSvc;
    }

    #endregion Ctors

    /// <inheritdoc/>
    public async Task<IEnumerable<SearchSuggestion>> FindSuggestionsAsync(
        string query,
        int length = 8,
        bool includeUsers = true,
        bool includeProducts = true)
    {
        var ret = new List<SearchSuggestion>();

        if (includeUsers)
        {
            var items = _userProfilesSvc
                .Search(query)
                .Include(u => u.User)
                .Page(1, length)
                .Select(u => new SearchSuggestion
                {
                    ItemId = u.Id.Id,
                    Title = u.FullName,
                    Type = $"user - @{u.User.Username}",
                })
                .AsAsyncEnumerable();

            ret.AddRange(await items.ToListAsync());
        }

        if (includeProducts)
        {
            var items = _productsSvc
                .Search(query)
                .Page(1, length)
                .Select(s => new SearchSuggestion
                {
                    ItemId = s.Id.Id,
                    Title = s.Title,
                    Type = "product",
                })
                .AsAsyncEnumerable();

            ret.AddRange(await items.ToListAsync());
        }

        ret.Sort((x, y) => x.Title.Length.CompareTo(y.Title.Length));

        return ret.Take(length);
    }
}