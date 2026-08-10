namespace RunLab.Core.Common;

public static class Statistics
{
    public static double Median(IEnumerable<double> values)
    {
        var sorted = values.OrderBy(v => v).ToArray();
        int n = sorted.Length;
        if (n == 0) return double.NaN;
        if (n % 2 == 1) return sorted[n / 2];
        return (sorted[n / 2 - 1] + sorted[n / 2]) / 2.0;
    }
}
