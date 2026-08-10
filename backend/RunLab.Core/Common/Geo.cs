using RunLab.Core.Models;

namespace RunLab.Core.Common;

public static class Geo
{
    public static double HaversineDistanceMeters(GeoPosition from, GeoPosition to)
    {
        const double R = 6371000;
        double dLat = (to.Latitude - from.Latitude) * Math.PI / 180;
        double dLon = (to.Longitude - from.Longitude) * Math.PI / 180;
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(from.Latitude * Math.PI / 180) * Math.Cos(to.Latitude * Math.PI / 180) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }
}
