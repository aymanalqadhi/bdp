namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent reservable product reservation window entity
/// </summary>
public sealed class ReservationWindow : AuditableEntity<ReservationWindow>
{
    /// <summary>
    /// Gets or sets the available days of the reservable product
    /// </summary>
    public Weekday AvailableDays { get; set; }

    /// <summary>
    /// Gets or sets the starting time of reservable product availability
    /// </summary>
    public TimeOnly Start { get; set; }

    /// <summary>
    /// Gets or sets the ending time of reservable product availability
    /// </summary>
    public TimeOnly End { get; set; }

    /// <summary>
    /// Gets or sets the reservable variant
    /// </summary>
    public ProductVariant Variant { get; set; } = null!;
}

/// <summary>
/// An enum to represent weekdays
/// </summary>
[Flags]
public enum Weekday : byte
{
    Saturday = 0x01,
    Sunday = 0x02,
    Monday = 0x04,
    Tuesday = 0x08,
    Wednesday = 0x10,
    Thursday = 0x20,
    Friday = 0x40,
}