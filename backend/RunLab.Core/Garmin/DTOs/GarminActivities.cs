using System.Text.Json.Serialization;

namespace RunLab.Core.Garmin.DTOs;

public class GarminActivities
{
    [JsonPropertyName("summarizedActivitiesExport")]
    public List<GarminActivity> Activities { get; set; } = [];
}
