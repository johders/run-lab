namespace RunLab.Core.Models.ActivitySummaries;

public class DistanceSummary
{
    public double DistanceMeters { get; init; }
    public double AverageSpeedMps { get; init; }
    public double MaxSpeedMps { get; init; }
    public double? AverageGradeAdjustedSpeedMps { get; init; }

    public double DistanceKilometers =>
        DistanceMeters / 1_000.0;

    public TimeSpan? AveragePacePerKm(TimeSpan? movingTime)
    {
        if (movingTime is null || DistanceMeters <= 0)
            return null;

        var minutesPerKm =
            movingTime.Value.TotalMinutes / DistanceKilometers;

        return TimeSpan.FromMinutes(minutesPerKm);
    }
}
