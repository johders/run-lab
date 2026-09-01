using RunLab.Core.Garmin;
using RunLab.Core.Models;

namespace RunLab.Tests.Garmin;

public class FitConverterTests
{
    [Fact]
    public void ToDegrees_WithNullValue_ReturnsNull()
    {
        int? inputValue = null;
        double? result = FitConverter.ToDegrees(inputValue);

        Assert.Null(result);
    }

    [Fact]
    public void ToDegrees_WithValidSemiCircleValue_ReturnsDegrees()
    {
        int? inputValue = 1 << 30;
        double? expected = 90;
        double? result = FitConverter.ToDegrees(inputValue);

        Assert.NotNull(result);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToDegrees_WithValidNegativeHelfSemiCircleValue_ReturnsDegrees()
    {
        int? inputValue = -(1 << 30);
        double? expected = -90;
        double? result = FitConverter.ToDegrees(inputValue);

        Assert.NotNull(result);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToDegrees_WithValidNegativeSemiCircleValue_ReturnsDegrees()
    {
        int? inputValue = int.MinValue;
        double? expected = -180;
        double? result = FitConverter.ToDegrees(inputValue);

        Assert.NotNull(result);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(1 << 30, null)]
    [InlineData(null, 1 << 30)]
    public void ToGeoPosition_WithNullValues_ReturnsNull(int? semiCirclesLatitude, int? semiCirclesLongitude)
    {
        GeoPosition? result = FitConverter.ToGeoPosition(semiCirclesLatitude, semiCirclesLongitude);

        Assert.Null(result);
    }

    [Fact]
    public void ToGeoPosition_WithValidValues_ReturnsGeoPosition()
    {
        int semiCirclesLatitude = 1 << 30;
        int semiCirclesLongitude = 1 << 29;

        double expectedLat = 90;
        double expectedLon = 45;

        GeoPosition? result = FitConverter.ToGeoPosition(semiCirclesLatitude, semiCirclesLongitude);

        Assert.NotNull(result);
        Assert.Equal(result.Latitude, expectedLat);
        Assert.Equal(result.Longitude, expectedLon);
    }
}
