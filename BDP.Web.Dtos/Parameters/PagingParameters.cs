using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Parameters;

public sealed class PagingParameters
{
    private const int _maxPageLength = 48;
    private const int _defaultPageLength = 16;

    /// <summary>
    /// Gets or sets the page index
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int Page { get; set; }

    /// <summary>
    /// Gets or sets the maximum page length
    /// </summary>

    [Range(1, _maxPageLength)]
    public int PageLength { get; set; } = _defaultPageLength;
}