using RunLab.Core.Models.DTOs;

namespace RunLab.Core.Services;

public class FitCalculationService
{
    public double CalculatePowerDecoupling(List<GarminFitRecord> records)
    {
        List<GarminFitRecord> validRecords = GetValidRecords(records);

        if (validRecords.Count < 1200) return 0;

        int midpoint = validRecords.Count / 2;
        List<GarminFitRecord> firstHalf = [.. validRecords.Take(midpoint)];
        List<GarminFitRecord> secondHalf = [.. validRecords.Skip(midpoint)];

        double ef1 = CalculateEfficiencyFactor(firstHalf);
        double ef2 = CalculateEfficiencyFactor(secondHalf);

        if (ef1 <= 0 || ef2 <= 0)
            return 0;

        double decoupling = ((ef1 - ef2) / ef1) * 100;
        //return Math.Max(0, decoupling);
        return decoupling;
    }

    public double CalculateCadenceDegradation(List<GarminFitRecord> records)
    {
        List<GarminFitRecord> validRecords = GetValidRecords(records);

        if (validRecords.Count < 1200) return 0;

        int midpoint = validRecords.Count / 2;

        double firstHalfAverageCadence = validRecords.Take(midpoint).Average(r => r.Cadence);
        double secondHalfAverageCadence = validRecords.Skip(midpoint).Average(r => r.Cadence);

        if (firstHalfAverageCadence <= 0 || secondHalfAverageCadence <= 0) return 0;

        return ((firstHalfAverageCadence - secondHalfAverageCadence) / firstHalfAverageCadence) * 100;
    }

    private double CalculateNormalizedPower(List<GarminFitRecord> segment)
    {
        double[] powerValues = [.. segment.Select(r => (double)r.Power)];

        if (powerValues.Length < 30)
            return powerValues.Length > 0 ? powerValues.Average() : 0;

        List<double> rollingAveragesRaised = [];

        double currentWindowSum = 0;
        for (int i = 0; i < powerValues.Length; i++)
        {
            currentWindowSum += powerValues[i];

            if (i >= 30)
                currentWindowSum -= powerValues[i - 30];

            if (i >= 29)
            {
                double avg = currentWindowSum / 30;
                rollingAveragesRaised.Add(Math.Pow(avg, 4));
            }
        }

        if (rollingAveragesRaised.Count == 0) 
            return segment.Average(r => r.Power);

        return Math.Pow(rollingAveragesRaised.Average(), 0.25);

    }

    private double CalculateEfficiencyFactor(List<GarminFitRecord> segment)
    {
        double avgHeartRate = segment.Average(r => r.HeartRate);
        double normPower = CalculateNormalizedPower(segment);
        return normPower / avgHeartRate;
    }

    private List<GarminFitRecord> GetValidRecords(List<GarminFitRecord> records)
    {
        return [.. records
            .Where(r => r.Power > 0 && r.HeartRate > 0 && r.Cadence > 0)
            .OrderBy(r => r.Timestamp!.GetDateTime())
            .Skip(600)];
    }
}
