using System.Text.Json.Serialization;

namespace RunLab.Core.Models;

public class GarminActivity
{
    [JsonPropertyName("activityId")]
    public long ActivityId { get; init; }

    [JsonPropertyName("sportType")]
    public string SportType { get; init; } = string.Empty;

    [JsonPropertyName("activityType")]
    public string ActivityType { get; init; } = string.Empty;

    [JsonIgnore]
    public bool IsRun =>
        SportType.Equals("RUNNING", StringComparison.OrdinalIgnoreCase) || 
        ActivityType.Equals("running", StringComparison.OrdinalIgnoreCase);

    [JsonPropertyName("startTimeGmt")]
    public double StartTimeGmtRaw { get; init; }

    [JsonIgnore]
    public DateTimeOffset StartTimeGmt =>
    DateTimeOffset.FromUnixTimeMilliseconds((long)StartTimeGmtRaw);

    [JsonPropertyName("startTimeLocal")]
    public double StartTimeLocalRaw { get; init; }

    [JsonIgnore]
    public DateTimeOffset StartTimeLocal =>
    DateTimeOffset.FromUnixTimeMilliseconds((long)StartTimeLocalRaw);

    [JsonPropertyName("duration")]
    public double DurationMs { get; init; }

    [JsonIgnore]
    public TimeSpan Duration => TimeSpan.FromMilliseconds(DurationMs);

    [JsonPropertyName("distance")]
    public double DistanceCm { get; init; }

    [JsonIgnore]
    public double DistanceKm => DistanceCm / 100_000.0;

    [JsonIgnore]
    public double PaceMinPerKm =>
        Duration.TotalMinutes / DistanceKm;

    [JsonPropertyName("avgSpeed")]
    public double AvgSpeed { get; init; }

    [JsonPropertyName("maxSpeed")]
    public double MaxSpeed { get; init; }

    [JsonPropertyName("avgHr")]
    public double AvgHr { get; init; }

    [JsonPropertyName("minHr")]
    public double MinHr { get; init; }

    [JsonPropertyName("maxHr")]
    public double MaxHr { get; init; }

    [JsonPropertyName("elevationGain")]
    public double ElevationGainCm { get; init; }

    [JsonPropertyName("elevationLoss")]
    public double ElevationLossCm { get; init; }

    [JsonPropertyName("steps")]
    public double Steps { get; init; }

    [JsonPropertyName("avgPower")]
    public double AvgPower { get; init; }

    [JsonPropertyName("maxPower")]
    public double MaxPower { get; init; }

    [JsonPropertyName("avgRunCadence")]
    public double AvgRunCadence { get; init; }

    [JsonPropertyName("maxRunCadence")]
    public double MaxRunCadence { get; init; }

    [JsonPropertyName("aerobicTrainingEffect")]
    public double AerobicTrainingEffect { get; init; }

    [JsonPropertyName("anaerobicTrainingEffect")]
    public double AnaerobicTrainingEffect { get; init; }

    [JsonPropertyName("trainingEffectLabel")]
    public string TrainingEffectLabel { get; init; } = string.Empty;

    [JsonPropertyName("activityTrainingLoad")]
    public double ActivityTrainingLoad { get; init; }

}
