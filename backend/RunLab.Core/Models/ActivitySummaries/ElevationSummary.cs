namespace RunLab.Core.Models.ActivitySummaries;

public class ElevationSummary
{
    public double TotalAscentMeters { get; init; }
    public double TotalDescentMeters { get; init; }
    public double? MinElevationMeters { get; init; }
    public double? MaxElevationMeters { get; init; }

    public double? NetElevationChangeMeters =>
        MinElevationMeters.HasValue && MaxElevationMeters.HasValue
            ? MaxElevationMeters.Value - MinElevationMeters.Value
            : null;

    public double? ClimbPerKm(double? distanceKm)
    {
        if (distanceKm is null || distanceKm <= 0)
            return null;

        return TotalAscentMeters / distanceKm.Value;
    }
}
