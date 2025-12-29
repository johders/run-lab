using RunLab.Core.Models.ActivitySummaries;
using RunLab.Core.Models.DTOs;

namespace RunLab.Core.Models;

public class GarminRunActivity
{
    public long ActivityId { get; init; }

    public ActivityMetaData Metadata { get; init; } = default!;
    public TimingSummary Timing { get; init; } = default!;
    public DistanceSummary Distance { get; init; } = default!;
    public ElevationSummary Elevation { get; init; } = default!;
    public HeartRateSummary HeartRate { get; init; } = default!;
    public PowerSummary Power { get; init; } = default!;
    public RunningDynamicsSummary RunningDynamics { get; init; } = default!;
    public TrainingEffectSummary TrainingEffect { get; init; } = default!;
    public EnergySummary Energy { get; init; } = default!;
    public LocationSummary Location { get; init; } = default!;

    public IReadOnlyList<GarminFitRecord> Records { get; init; } = [];
}
