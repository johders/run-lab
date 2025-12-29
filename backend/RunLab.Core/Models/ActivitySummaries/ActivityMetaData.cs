namespace RunLab.Core.Models.ActivitySummaries;

public class ActivityMetaData
{
    public string Name { get; init; } = string.Empty;
    public string SportType { get; init; } = string.Empty;
    public string ActivityType { get; init; } = string.Empty;

    public bool IsRun =>
        SportType.Equals("RUNNING", StringComparison.OrdinalIgnoreCase) ||
        ActivityType.Equals("running", StringComparison.OrdinalIgnoreCase);
}
