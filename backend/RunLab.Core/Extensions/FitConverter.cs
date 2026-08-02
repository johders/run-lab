using RunLab.Core.Models.DTOs;

namespace RunLab.Core.Extensions;

public static class FitConverter
{
    private const double ConversionScaleFactor = 180.0 / 2147483648.0;
    public static double? ToDegrees(this int? semiCircles)
    {
        if (semiCircles is null)
        {
            return null;
        }

        return semiCircles * ConversionScaleFactor;
    }

    public static GeoPosition? ToGeoPosition(int? semiCirclesLatitude, int? semiCirclesLongitude)
    {

        if (semiCirclesLatitude.ToDegrees() is double latitude &&
            semiCirclesLongitude.ToDegrees() is double longitude)
        {
            return new GeoPosition(latitude, longitude);
        }

        return null;
    }
}
