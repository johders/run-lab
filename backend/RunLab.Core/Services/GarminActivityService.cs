using RunLab.Core.Models;
using System.Text.Json;

namespace RunLab.Core.Services;

public class GarminActivityService(string jsonFilePath)
{
    public async Task<List<GarminActivity>> LoadActivitiesAsync()
    {
        if (!File.Exists(jsonFilePath))
        {
            throw new FileNotFoundException($"Activities JSON file not found at: {jsonFilePath}");
        }

        using FileStream stream = File.OpenRead(jsonFilePath);

        var wrapperArray = await JsonSerializer.DeserializeAsync<List<GarminActivities>>(stream);

        if (wrapperArray is null)
        {
            return [];
        }

        return [.. wrapperArray.SelectMany(w => w.Activities)];
    }

    public async Task<List<GarminActivity>> LoadRunsAsync()
    {
        List<GarminActivity> allActivities = await LoadActivitiesAsync();
        return [.. allActivities.Where(a => a.IsRun)];
    }
}
