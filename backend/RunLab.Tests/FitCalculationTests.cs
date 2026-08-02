using RunLab.Core.Services;
using RunLab.Core.Extensions;
using RunLab.Core.Models.DTOs;

namespace RunLab.Tests;

public class FitCalculationTests
{
    [Fact]
    public void CalculateMedian_WithEvenNumberedList_ReturnCorrectMedian()
    {

        List<double> evenNumberedist = [2, 3, 4, 5];
        double expectedResult = 3.5;

        double testResult = FitCalculationService.Median(evenNumberedist);

        Assert.Equal(expectedResult, testResult);
    }

    [Fact]
    public void CalculateMedian_WithUnEvenNumberedList_ReturnCorrectMedian()
    {
        IEnumerable<double> unEvenNumberedist = [2, 3, 4];
        double expectedResult = 3;

        double testResult = FitCalculationService.Median(unEvenNumberedist);

        Assert.Equal(expectedResult, testResult);
    }

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
        int? inputValue = (int)Math.Pow(2, 30);
        double? expected = 90;
        double? result = FitConverter.ToDegrees(inputValue);

        Assert.NotNull(result);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToDegrees_WithValidNegativeHelfSemiCircleValue_ReturnsDegrees()
    {
        int? inputValue = -(int)Math.Pow(2, 30);
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
    [InlineData(0,0,0,0,0)]
    [InlineData(0, 0, 1, 0, 111194.93)]
    [InlineData(0, 0, 0, 1, 111194.93)]
    [InlineData(60, 0, 60, 1, 55597)]
    public void HaversineDistanceMeters_WithValidValues_ReturnsGreatCircleDistance(
        double lat1, double lon1, double lat2, double lon2, double expected)
    {
        GeoPosition from = new(lat1, lon1);
        GeoPosition to = new(lat2, lon2);

        double resultOneWay = FitCalculationService.HaversineDistanceMeters(from.Latitude, from.Longitude, to.Latitude, to.Longitude);
        double resultReturn = FitCalculationService.HaversineDistanceMeters(from.Latitude, from.Longitude, to.Latitude, to.Longitude);

        Assert.Equal(resultOneWay, resultReturn);
        Assert.Equal(expected, resultReturn, tolerance: 0.5);
        Assert.Equal(expected, resultReturn, tolerance: 0.5);

    }

    [Theory]
    [InlineData(-1, -1, -2, -2)]
    public void HaversineDistanceMeters_WithNegativeValues_IsGreaterThanZero(
        double lat1, double lon1, double lat2, double lon2)
    {
        GeoPosition from = new(lat1, lon1);
        GeoPosition to = new(lat2, lon2);

        double result = FitCalculationService.HaversineDistanceMeters(from.Latitude, from.Longitude, to.Latitude, to.Longitude);

        Assert.True(result > 0);
    }
}
