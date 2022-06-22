namespace BDP.Web.Dtos.Requests;

public class CreateReservationWindowRequest
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