namespace RunLab.Core.Models.ActivitySummaries;

public class RunningDynamicsSummary
{    
    public double? AverageCadenceSpm { get; init; }
    public double? MaxCadenceSpm { get; init; }
    public double? AverageSingleLegCadenceSpm { get; init; }
    public double? AverageStrideLengthMeters { get; init; }
    public double? AverageGroundContactTimeMs { get; init; }
    public double? AverageVerticalOscillationCm { get; init; }
    public double? AverageVerticalRatioPercent { get; init; }
    public double? MaxVerticalSpeedMps { get; init; }

    public double? StepLengthMeters =>
        AverageStrideLengthMeters.HasValue
            ? AverageStrideLengthMeters / 2.0
            : null;

    public double? CadenceStrideProduct =>
        AverageCadenceSpm.HasValue && AverageStrideLengthMeters.HasValue
            ? AverageCadenceSpm * AverageStrideLengthMeters
            : null;

    public bool HasRunningDynamics =>
        AverageCadenceSpm.HasValue ||
        AverageStrideLengthMeters.HasValue ||
        AverageGroundContactTimeMs.HasValue;
}
