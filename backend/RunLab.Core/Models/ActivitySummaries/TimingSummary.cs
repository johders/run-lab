namespace RunLab.Core.Models.ActivitySummaries;

public class TimingSummary
{
    public DateTimeOffset StartTimeUtc { get; init; }
    public DateTimeOffset StartTimeLocal { get; init; }

    public TimeSpan Duration { get; init; }
    public TimeSpan MovingTime { get; init; }
    public TimeSpan ElapsedTime { get; init; }
}
