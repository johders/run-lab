using RunLab.Core.Models.DTOs;

namespace RunLab.Core.Services;

public class FitCalculationService
{
    private const int WarmupSeconds = 600;
    private const int MinimumRecords = 1200;
    public static double CalculatePowerDecoupling(List<GarminFitRecord> records)
    {
        List<GarminFitRecord> validRecords = GetValidRecords(records);

        if (validRecords.Count < MinimumRecords) 
            return double.NaN;

        int midpoint = validRecords.Count / 2;
        List<GarminFitRecord> firstHalf = [.. validRecords.Take(midpoint)];
        List<GarminFitRecord> secondHalf = [.. validRecords.Skip(midpoint)];

        double ef1 = CalculateEfficiencyFactor(firstHalf);
        double ef2 = CalculateEfficiencyFactor(secondHalf);

        if (ef1 <= 0 || ef2 <= 0)
            return double.NaN;

        double decoupling = ((ef1 - ef2) / ef1) * 100;
        //return Math.Max(0, decoupling);
        return decoupling;
    }

    public static double CalculateCadenceDegradation(List<GarminFitRecord> records)
    {
        List<GarminFitRecord> validRecords = GetValidRecords(records);

        if (validRecords.Count < MinimumRecords)
            return double.NaN;

        int midpoint = validRecords.Count / 2;

        double firstHalfAverageCadence = validRecords.Take(midpoint).Average(r => r.Cadence);
        double secondHalfAverageCadence = validRecords.Skip(midpoint).Average(r => r.Cadence);

        if (firstHalfAverageCadence <= 0 || secondHalfAverageCadence <= 0)
            return double.NaN;

        return ((firstHalfAverageCadence - secondHalfAverageCadence) / firstHalfAverageCadence) * 100;
    }

    public static double CalculateAverageGradeAdjustedPace(List<GarminFitRecord> records)
    {
        List<GarminFitRecord> validRecords = GetValidRecords(records);

        if (validRecords.Count < 2)
            return double.NaN;

        List<double> gapSpeeds = new List<double>();
        for (int i = 1; i < validRecords.Count; i++)
        {
            double gap = CalculateGapForSegment(validRecords[i - 1], validRecords[i]);
            if (gap > 0) gapSpeeds.Add(gap);
        }

        if (gapSpeeds.Count == 0)
            return double.NaN;

        return Median(gapSpeeds);
    }

    private static double CalculateNormalizedPower(List<GarminFitRecord> segment)
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

    private static double CalculateEfficiencyFactor(List<GarminFitRecord> segment)
    {
        double avgHeartRate = segment.Average(r => r.HeartRate);
        double normPower = CalculateNormalizedPower(segment);
        return normPower / avgHeartRate;
    }

    private static List<GarminFitRecord> GetValidRecords(List<GarminFitRecord> records)
    {
        return [.. records
            .Where(r => r.Power > 0 && r.HeartRate > 0 && r.Cadence > 0)
            .OrderBy(r => r.Timestamp!.GetDateTime())
            .Skip(WarmupSeconds)];
    }

    private static double HaversineDistanceMeters(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371000;
        double dLat = (lat2 - lat1) * Math.PI / 180;
        double dLon = (lon2 - lon1) * Math.PI / 180;
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }

    private static double GradeEnergyMultiplier(double grade)
    {
        grade = Math.Clamp(grade, -0.25, 0.25);
        return 1 + 6.0 * grade + 30.0 * grade * grade;
    }

    private static double CalculateGapForSegment(GarminFitRecord prev, GarminFitRecord curr)
    {
        double distance = HaversineDistanceMeters(prev.Latitude, prev.Longitude, curr.Latitude, curr.Longitude);
        if (distance < 0.5) return 0;

        double timeSeconds = (curr.Timestamp!.GetDateTime() - prev.Timestamp!.GetDateTime()).TotalSeconds;
        if (timeSeconds <= 0) return 0;

        double speed = distance / timeSeconds;
        double grade = (curr.Altitude - prev.Altitude) / distance;
        double multiplier = GradeEnergyMultiplier(grade);

        return speed / multiplier;
    }

    private static double Median(IEnumerable<double> values)
    {
        var sorted = values.OrderBy(v => v).ToArray();
        int n = sorted.Length;
        if (n == 0) return double.NaN;
        if (n % 2 == 1) return sorted[n / 2];
        return (sorted[n / 2 - 1] + sorted[n / 2]) / 2.0;
    }
}
