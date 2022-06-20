namespace BDP.Domain.ValueTypes;

public class TimeRange : ValueType
{
    /// <summary>
    /// Defaults constructor
    /// </summary>
    public TimeRange() : this(new DateTime(0, 0, 0, 0, 0, 0), new DateTime(0, 0, 0, 23, 59, 59))
    {
    }

    /// <summary>
    /// A constructor to specifiy the begin and end values
    /// of the time range
    /// </summary>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    public TimeRange(DateTime begin, DateTime end)
    {
        Begin = begin;
        End = end;
    }

    /// <summary>
    /// Gets or sets the begining time of the time range
    /// </summary>
    public DateTime Begin { get; set; }

    /// <summary>
    /// Gets or sets the ending time of the time range
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// Gets a time range that convers all day
    /// </summary>
    public static TimeRange AllDay => new();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Begin;
        yield return End;
    }
}
