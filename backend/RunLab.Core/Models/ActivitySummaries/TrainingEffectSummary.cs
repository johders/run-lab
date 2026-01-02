namespace RunLab.Core.Models.ActivitySummaries;

public class TrainingEffectSummary
{
    public double? AerobicTrainingEffect { get; init; }
    public double? AnaerobicTrainingEffect { get; init; }
    public double? TrainingLoad { get; init; }
    public double? Vo2MaxEstimate { get; init; }
    public string? TrainingEffectLabel { get; init; }
    public string? AerobicEffectMessage { get; init; }
    public string? AnaerobicEffectMessage { get; init; }
    public bool IsPrimarilyAerobic =>
        AerobicTrainingEffect.HasValue &&
        (!AnaerobicTrainingEffect.HasValue ||
         AerobicTrainingEffect >= AnaerobicTrainingEffect);

    public bool IsHighIntensity =>
        AnaerobicTrainingEffect.HasValue &&
        AnaerobicTrainingEffect >= 3.0;

    public bool HasMeaningfulStimulus =>
        (AerobicTrainingEffect ?? 0) >= 1.0 ||
        (AnaerobicTrainingEffect ?? 0) >= 1.0;
}
