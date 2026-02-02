namespace RunLab.Core.Models.ML;

public class RunMLFeatures(long activityId, DateTime date, double hrDecoupling, double cadenceDegradation, double gradeAdjustedPace)
{
    public long ActivityId { get; init; } = activityId;
    public DateTime Date { get; init; } = date;
    public double HrDecoupling { get; init; } = hrDecoupling;
    public double CadenceDegradation { get; init; } = cadenceDegradation;
    public double GradeAdjustedPace { get; init; } = gradeAdjustedPace;
}
