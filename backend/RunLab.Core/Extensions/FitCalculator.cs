namespace RunLab.Core.Extensions
{
    public static class FitCalculator
    {
        public static double ToDegrees(this int? semiCircles)
        {
            if (semiCircles is null)
            {
                return 0;
            }

            return (double)semiCircles * (180 / Math.Pow(2, 31));
        }
    }
}
