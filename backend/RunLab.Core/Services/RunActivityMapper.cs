using RunLab.Core.Models;
using RunLab.Core.Models.ActivitySummaries;
using RunLab.Core.Models.DTOs;

namespace RunLab.Core.Services;

public static class RunActivityMapper
{
    public static List<GarminRunActivity> ToGarminRunActivities(this List<GarminActivity> activities)
    {
        return [.. activities.Select(a => ToGarminRunActivity(a))];
    }
    public static GarminRunActivity ToGarminRunActivity(this GarminActivity activity)
    {
        return new GarminRunActivity
        {
            Metadata = new ActivityMetaData
            {
                Name = activity.Name,
                SportType = activity.SportType,
                ActivityType = activity.ActivityType
            },
            Timing = new TimingSummary
            {
                StartTimeUtc = activity.StartTimeGmt,
                StartTimeLocal = activity.StartTimeLocal,
                Duration = activity.ElapsedDuration,
                MovingTime = activity.MovingDuration
            },
            Distance = new DistanceSummary
            {
                DistanceMeters = activity.DistanceMeters,
                AverageSpeedMps = activity.AvgSpeedMps,
                MaxSpeedMps = activity.MaxSpeedMps,
                AverageGradeAdjustedSpeedMps = activity.AvgGradeAdjustedSpeedMps > 0 ? activity.AvgGradeAdjustedSpeedMps : null
            },
            Elevation = new ElevationSummary
            {
                TotalAscentMeters = activity.ElevationGainMeters,
                TotalDescentMeters = activity.ElevationLossMeters,
                MinElevationMeters = activity.MinElevationCm > 0 ? activity.MinElevationCm / 100.0 : null,
                MaxElevationMeters = activity.MaxElevationCm > 0 ? activity.MaxElevationCm / 100.0 : null
            },
            HeartRate = new HeartRateSummary
            {
                AverageBpm = activity.AvgHr,
                MinBpm = activity.MinHr,
                MaxBpm = activity.MaxHr,
                TimeInZones = new Dictionary<int, TimeSpan>
                {
                    {0, TimeSpan.FromSeconds(activity.HrZone0Seconds)},
                    {1, TimeSpan.FromSeconds(activity.HrZone1Seconds)},
                    {2, TimeSpan.FromSeconds(activity.HrZone2Seconds)},
                    {3, TimeSpan.FromSeconds(activity.HrZone3Seconds)},
                    {4, TimeSpan.FromSeconds(activity.HrZone4Seconds)},
                    {5, TimeSpan.FromSeconds(activity.HrZone5Seconds)},
                    {6, TimeSpan.FromSeconds(activity.HrZone6Seconds)},
                }
                .Where(kvp => kvp.Value > TimeSpan.Zero)
                .ToDictionary()
            },
            Power = new PowerSummary
            {
                AverageWatts = activity.AvgPower,
                MaxWatts = activity.MaxPower,
                NormalizedWatts = activity.NormalizedPower,
                WindCorrectionEnabled = activity.IsRunPowerWindDataEnabled,
                TimeInZones = new Dictionary<int, TimeSpan>
                {
                    { 0, TimeSpan.FromSeconds(activity.PowerZone0Seconds) },
                    { 1, TimeSpan.FromSeconds(activity.PowerZone1Seconds) },
                    { 2, TimeSpan.FromSeconds(activity.PowerZone2Seconds) },
                    { 3, TimeSpan.FromSeconds(activity.PowerZone3Seconds) },
                    { 4, TimeSpan.FromSeconds(activity.PowerZone4Seconds) },
                    { 5, TimeSpan.FromSeconds(activity.PowerZone5Seconds) },
                }
                .Where(kvp => kvp.Value > TimeSpan.Zero)
                .ToDictionary()
            },
            RunningDynamics = new RunningDynamicsSummary
            {
                AverageCadenceSpm = activity.AvgDoubleCadenceSpm,
                MaxCadenceSpm = activity.MaxDoubleCadenceSpm,
                AverageSingleLegCadenceSpm = activity.AvgRunCadenceSpm,
                AverageStrideLengthMeters = activity.AvgStrideLengthCm / 100,
                AverageGroundContactTimeMs = activity.AvgGroundContactTimeMs,
                AverageVerticalOscillationCm = activity.AvgVerticalOscillationCm,
                AverageVerticalRatioPercent = activity.AvgVerticalRatioPercent,
                MaxVerticalSpeedMps = activity.MaxVerticalSpeedMps
            },
            TrainingEffect = new TrainingEffectSummary
            {
                AerobicTrainingEffect = activity.AerobicTrainingEffect,
                AnaerobicTrainingEffect = activity.AnaerobicTrainingEffect,
                TrainingLoad = activity.ActivityTrainingLoad,
                Vo2MaxEstimate = activity.Vo2MaxValue,
                TrainingEffectLabel = activity.TrainingEffectLabel,
                AerobicEffectMessage = activity.AerobicTrainingEffectMessage,
                AnaerobicEffectMessage = activity.AnaerobicTrainingEffectMessage
            },
            Energy = new EnergySummary
            {
                ModerateIntensityMinutes = activity.ModerateIntensityMinutes,
                VigorousIntensityMinutes = activity.VigorousIntensityMinutes,
                BodyBatteryDelta = activity.BodyBatteryDelta,
                WorkoutFeel = activity.WorkoutFeel,
                WorkoutRpe = activity.WorkoutRpe,
                Calories = activity.Calories,
                BmrCalories = activity.BmrCalories,
                WaterEstimatedMl = activity.WaterEstimatedMl
            },
            Location = new LocationSummary
            {
                LocationName = activity.LocationName,
                Start = new GeoPoint(activity.StartLatitude, activity.StartLongitude),
                End = new GeoPoint(activity.EndLatitude, activity.EndLongitude),
                BoundingBox = new GeoBoundingBox
                {
                    MinLatitude = activity.MinLatitude,
                    MaxLatitude = activity.MaxLatitude,
                    MinLongitude = activity.MinLongitude,
                    MaxLongitude = activity.MaxLongitude
                }
            }
        };
    }
}
