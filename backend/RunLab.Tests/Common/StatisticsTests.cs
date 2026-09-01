using RunLab.Core.Common;

namespace RunLab.Tests.Common;

public class StatisticsTests
{
    [Fact]
    public void CalculateMedian_WithEvenNumberedList_ReturnCorrectMedian()
    {

        List<double> evenNumberedist = [2, 3, 4, 5];
        double expectedResult = 3.5;

        double testResult = Statistics.Median(evenNumberedist);

        Assert.Equal(expectedResult, testResult);
    }

    [Fact]
    public void CalculateMedian_WithUnEvenNumberedList_ReturnCorrectMedian()
    {
        IEnumerable<double> unEvenNumberedist = [2, 3, 4];
        double expectedResult = 3;

        double testResult = Statistics.Median(unEvenNumberedist);

        Assert.Equal(expectedResult, testResult);
    }
}
