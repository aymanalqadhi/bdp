using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A data-transfer object for <see cref="ProductVariant"/> with reservable type
/// </summary>
public sealed class ReservationWindowDto : MutableEntityDto<ReservationWindow>
{
    /// <summary>
    /// Gets or sets the available days of the reservable product
    /// </summary>
    public byte AvailableDays { get; set; }

    /// <summary>
    /// Gets or sets the starting time of reservable product availability
    /// </summary>
    public TimeSpan Start { get; set; }

    /// <summary>
    /// Gets or sets the ending time of reservable product availability
    /// </summary>
    public TimeSpan End { get; set; }
}