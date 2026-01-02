namespace RunLab.Core.Models.ActivitySummaries;

public class EnergySummary
{
    public int ModerateIntensityMinutes { get; init; }
    public int VigorousIntensityMinutes { get; init; }
    public int TotalIntensityMinutes =>
        ModerateIntensityMinutes + VigorousIntensityMinutes;

    public int BodyBatteryDelta { get; init; }
    public int WorkoutFeel { get; init; }
    public int WorkoutRpe { get; init; }

    public double Calories { get; init; }
    public double BmrCalories { get; init; }
    public double WaterEstimatedMl { get; init; }
    public double CaloriesPerKm(double distanceKm) =>
        distanceKm > 0 ? Calories / distanceKm : 0;
}
