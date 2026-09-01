using RunLab.Core.Common;
using RunLab.Core.Models;

namespace RunLab.Tests.Common;

public class GeoTests
{
    [Theory]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(0, 0, 1, 0, 111194.93)]
    [InlineData(0, 0, 0, 1, 111194.93)]
    [InlineData(60, 0, 60, 1, 55597)]
    public void HaversineDistanceMeters_WithValidValues_ReturnsGreatCircleDistance(
    double lat1, double lon1, double lat2, double lon2, double expected)
    {
        GeoPosition from = new(lat1, lon1);
        GeoPosition to = new(lat2, lon2);

        double result = Geo.HaversineDistanceMeters(from, to);

        Assert.Equal(expected, result, tolerance: 0.5);

    }

    [Fact]
    public void HaversineDistanceMeters_WithSignedValues_IsSameDistance()
    {
        GeoPosition from1 = new(-1, -1);
        GeoPosition to1 = new(-2, -2);

        GeoPosition from2 = new(1, 1);
        GeoPosition to2 = new(2, 2);

        double result1 = Geo.HaversineDistanceMeters(from1, to1);
        double result2 = Geo.HaversineDistanceMeters(from2, to2);

        Assert.Equal(result1, result2);
    }

    [Fact]
    public void HaversineDistanceMeters_WithValidValues_IsSameDistanceBothWays()
    {
        GeoPosition from = new(4, 1);
        GeoPosition to = new(5, 2);

        double resultOneWay = Geo.HaversineDistanceMeters(from, to);
        double resultReturn = Geo.HaversineDistanceMeters(to, from);

        Assert.Equal(resultOneWay, resultReturn);
    }
}
