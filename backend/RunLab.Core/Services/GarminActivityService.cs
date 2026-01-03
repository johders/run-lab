using RunLab.Core.Models;
using RunLab.Core.Models.DTOs;
using System.Text.Json;

namespace RunLab.Core.Services;

public class GarminActivityService
{
    private readonly GarminFitParser _parser;
    private readonly string _jsonFilePath;
    public GarminActivityService(string jsonFilePath, string fitFileRoot)
    {
        _jsonFilePath = jsonFilePath;
        _parser = new GarminFitParser(fitFileRoot);
    }
    public async Task<List<GarminActivity>> LoadActivitiesAsync()
    {
        if (!File.Exists(_jsonFilePath))
        {
            throw new FileNotFoundException($"Activities JSON file not found at: {_jsonFilePath}");
        }

        using FileStream stream = File.OpenRead(_jsonFilePath);

        var wrapperArray = await JsonSerializer.DeserializeAsync<List<GarminActivities>>(stream);

        if (wrapperArray is null)
        {
            return [];
        }

        return [.. wrapperArray.SelectMany(w => w.Activities)];
    }

    public async Task<List<GarminRunActivity>> LoadRunsAsync()
    {
        List<GarminActivity> allActivities = await LoadActivitiesAsync();

        ILookup<long, GarminFitActivity> fitLookup = _parser.LoadAllActivities().ToLookup(f => f.Id);

        return [.. allActivities
            .Where(a => a.ActivityType == "running")
            .Where(a => fitLookup.Contains(a.ActivityId))
            .Select(a => a.ToGarminRunActivity(fitLookup[a.ActivityId].Single()))];
    }
}
