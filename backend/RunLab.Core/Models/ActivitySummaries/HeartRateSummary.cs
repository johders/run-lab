namespace RunLab.Core.Models.ActivitySummaries;

public class HeartRateSummary
{
    public double? AverageBpm { get; init; }
    public double? MinBpm { get; init; }
    public double? MaxBpm { get; init; }

    public IReadOnlyDictionary<int, TimeSpan> TimeInZones { get; init; }
        = new Dictionary<int, TimeSpan>();

    public TimeSpan TotalTimeInZones =>
        TimeInZones.Values.Aggregate(TimeSpan.Zero, (a, b) => a + b);

    public double? ZonePercentage(int zone)
    {
        if (!TimeInZones.TryGetValue(zone, out var zoneTime))
            return null;

        var total = TotalTimeInZones;
        if (total.TotalSeconds <= 0)
            return null;

        return zoneTime.TotalSeconds / total.TotalSeconds;
    }

    public bool HasHeartRateData =>
        AverageBpm.HasValue || TimeInZones.Count > 0;
}
