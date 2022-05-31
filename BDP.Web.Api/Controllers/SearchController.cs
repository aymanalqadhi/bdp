using BDP.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    #region Private fields

    private readonly ISearchSuggestionsService _searchSuggestionsSvc;

    #endregion Private fields

    #region Ctors

    public SearchController(ISearchSuggestionsService searchSuggestionsSvc)
    {
        _searchSuggestionsSvc = searchSuggestionsSvc;
    }

    #endregion Ctors

    #region Actions

    [HttpGet]
    public async Task<IActionResult> Suggestions(string query)
        => Ok(await _searchSuggestionsSvc.FindSuggestionsAsync(query));

    #endregion Actions
}