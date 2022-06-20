namespace BDP.Web.Dtos;

public class ServiceDto : SellableDto
{
    /// <summary>
    /// Gets or sets the availability begining time of the service
    /// (to be changed with <see cref="TimeOnly"/>)
    /// </summary>
    public DateTime AvailableBegin { get; set; }

    /// <summary>
    /// Gets or sets the availability begining time of the service
    /// (to be changed with <see cref="TimeOnly"/>)
    /// </summary>
    public DateTime AvailableEnd { get; set; }
}