using System.Text.Json.Serialization;

namespace RunLab.Core.Models.DTOs;

public class GarminActivity
{
    // Identity & activity

    [JsonPropertyName("activityId")]
    public long ActivityId { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("sportType")]
    public string SportType { get; init; } = string.Empty;

    [JsonPropertyName("activityType")]
    public string ActivityType { get; init; } = string.Empty;

    // Time

    [JsonPropertyName("timeZoneId")]
    public int TimeZoneId { get; init; }

    [JsonPropertyName("beginTimestamp")]
    public long BeginTimestampMs { get; init; }

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

    [JsonPropertyName("elapsedDuration")]
    public double ElapsedDurationMs { get; init; }

    [JsonIgnore]
    public TimeSpan ElapsedDuration => TimeSpan.FromMilliseconds(ElapsedDurationMs);

    [JsonPropertyName("movingDuration")]
    public double MovingDurationMs { get; init; }

    [JsonIgnore]
    public TimeSpan MovingDuration => TimeSpan.FromMilliseconds(MovingDurationMs);

    // Distance & speed

    [JsonPropertyName("distance")]
    public double DistanceCm { get; init; }

    [JsonIgnore]
    public double DistanceMeters => DistanceCm / 100.0;

    [JsonIgnore]
    public double DistanceKm => DistanceCm / 100_000.0;

    [JsonIgnore]
    public double PaceMinPerKm =>
        DistanceKm > 0 ? Duration.TotalMinutes / DistanceKm : 0;

    [JsonPropertyName("avgSpeed")]
    public double AvgSpeedMps { get; init; }

    [JsonPropertyName("maxSpeed")]
    public double MaxSpeedMps { get; init; }

    [JsonPropertyName("avgGradeAdjustedSpeed")]
    public double AvgGradeAdjustedSpeedMps { get; init; }

    // Elevation

    [JsonPropertyName("elevationGain")]
    public double ElevationGainCm { get; init; }

    [JsonPropertyName("elevationLoss")]
    public double ElevationLossCm { get; init; }

    [JsonIgnore]
    public double ElevationGainMeters => ElevationGainCm / 100.0;

    [JsonIgnore]
    public double ElevationLossMeters => ElevationLossCm / 100.0;

    [JsonPropertyName("minElevation")]
    public double MinElevationCm { get; init; }

    [JsonPropertyName("maxElevation")]
    public double MaxElevationCm { get; init; }

    // Heart rate

    [JsonPropertyName("avgHr")]
    public double AvgHr { get; init; }

    [JsonPropertyName("minHr")]
    public double MinHr { get; init; }

    [JsonPropertyName("maxHr")]
    public double MaxHr { get; init; }

    [JsonPropertyName("hrTimeInZone_0")]
    public int HrZone0Seconds { get; init; }

    [JsonPropertyName("hrTimeInZone_1")]
    public int HrZone1Seconds { get; init; }

    [JsonPropertyName("hrTimeInZone_2")]
    public int HrZone2Seconds { get; init; }

    [JsonPropertyName("hrTimeInZone_3")]
    public int HrZone3Seconds { get; init; }

    [JsonPropertyName("hrTimeInZone_4")]
    public int HrZone4Seconds { get; init; }

    [JsonPropertyName("hrTimeInZone_5")]
    public int HrZone5Seconds { get; init; }

    [JsonPropertyName("hrTimeInZone_6")]
    public int HrZone6Seconds { get; init; }

    // Power

    [JsonPropertyName("avgPower")]
    public double AvgPower { get; init; }

    [JsonPropertyName("maxPower")]
    public double MaxPower { get; init; }

    [JsonPropertyName("normPower")]
    public double NormalizedPower { get; init; }

    [JsonPropertyName("isRunPowerWindDataEnabled")]
    public bool IsRunPowerWindDataEnabled { get; init; }

    [JsonPropertyName("powerTimeInZone_0")]
    public int PowerZone0Seconds { get; init; }

    [JsonPropertyName("powerTimeInZone_1")]
    public int PowerZone1Seconds { get; init; }

    [JsonPropertyName("powerTimeInZone_2")]
    public int PowerZone2Seconds { get; init; }

    [JsonPropertyName("powerTimeInZone_3")]
    public int PowerZone3Seconds { get; init; }

    [JsonPropertyName("powerTimeInZone_4")]
    public int PowerZone4Seconds { get; init; }

    [JsonPropertyName("powerTimeInZone_5")]
    public int PowerZone5Seconds { get; init; }

    // Running dynamics

    [JsonPropertyName("avgRunCadence")]
    public double AvgRunCadenceSpm { get; init; }

    [JsonPropertyName("maxRunCadence")]
    public double MaxRunCadenceSpm { get; init; }

    [JsonPropertyName("avgDoubleCadence")]
    public double AvgDoubleCadenceSpm { get; init; }

    [JsonPropertyName("maxDoubleCadence")]
    public double MaxDoubleCadenceSpm { get; init; }

    [JsonPropertyName("avgStrideLength")]
    public double AvgStrideLengthCm { get; init; }

    [JsonPropertyName("avgVerticalOscillation")]
    public double AvgVerticalOscillationCm { get; init; }

    [JsonPropertyName("avgGroundContactTime")]
    public double AvgGroundContactTimeMs { get; init; }

    [JsonPropertyName("avgVerticalRatio")]
    public double AvgVerticalRatioPercent { get; init; }

    [JsonPropertyName("maxVerticalSpeed")]
    public double MaxVerticalSpeedMps { get; init; }

    // Training effect & physiology

    [JsonPropertyName("aerobicTrainingEffect")]
    public double AerobicTrainingEffect { get; init; }

    [JsonPropertyName("anaerobicTrainingEffect")]
    public double AnaerobicTrainingEffect { get; init; }

    [JsonPropertyName("trainingEffectLabel")]
    public string TrainingEffectLabel { get; init; } = string.Empty;

    [JsonPropertyName("activityTrainingLoad")]
    public double ActivityTrainingLoad { get; init; }

    [JsonPropertyName("aerobicTrainingEffectMessage")]
    public string AerobicTrainingEffectMessage { get; init; } = string.Empty;

    [JsonPropertyName("anaerobicTrainingEffectMessage")]
    public string AnaerobicTrainingEffectMessage { get; init; } = string.Empty;

    [JsonPropertyName("vO2MaxValue")]
    public double Vo2MaxValue { get; init; }

    // Intensity & subjective

    [JsonPropertyName("moderateIntensityMinutes")]
    public int ModerateIntensityMinutes { get; init; }

    [JsonPropertyName("vigorousIntensityMinutes")]
    public int VigorousIntensityMinutes { get; init; }

    [JsonPropertyName("differenceBodyBattery")]
    public int BodyBatteryDelta { get; init; }

    [JsonPropertyName("workoutFeel")]
    public int WorkoutFeel { get; init; }

    [JsonPropertyName("workoutRpe")]
    public int WorkoutRpe { get; init; }

    // Energy & hydration

    [JsonPropertyName("calories")]
    public double Calories { get; init; }

    [JsonPropertyName("bmrCalories")]
    public double BmrCalories { get; init; }

    [JsonPropertyName("waterEstimated")]
    public double WaterEstimatedMl { get; init; }

    // --------------------
    // Location
    // --------------------

    [JsonPropertyName("locationName")]
    public string LocationName { get; init; } = string.Empty;

    [JsonPropertyName("startLatitude")]
    public double StartLatitude { get; init; }

    [JsonPropertyName("startLongitude")]
    public double StartLongitude { get; init; }

    [JsonPropertyName("endLatitude")]
    public double EndLatitude { get; init; }

    [JsonPropertyName("endLongitude")]
    public double EndLongitude { get; init; }

    [JsonPropertyName("minLatitude")]
    public double MinLatitude { get; init; }

    [JsonPropertyName("maxLatitude")]
    public double MaxLatitude { get; init; }

    [JsonPropertyName("minLongitude")]
    public double MinLongitude { get; init; }

    [JsonPropertyName("maxLongitude")]
    public double MaxLongitude { get; init; }
}
