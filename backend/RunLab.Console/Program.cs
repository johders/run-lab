using Microsoft.Extensions.Configuration;
using RunLab.Core.Models.DTOs;
using RunLab.Core.Services;

IConfigurationBuilder builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

IConfiguration config = builder.Build();

string jsonPath = config["Garmin:ActivitiesPath"] ?? default!;
string fitFileRoot = config["Garmin:FitFilesRoot"] ?? default!;

GarminActivityService activitiesService = new (jsonPath);
GarminFitParser fitService = new (fitFileRoot);

List<GarminFitActivity> myFitActivities = [.. fitService.LoadAllActivities()];
HashSet<long> fitActivityIds = [.. myFitActivities.Select(fa => fa.Id)];


List<GarminActivity> allActivities = await activitiesService.LoadActivitiesAsync();
List<GarminActivity> allRuns = await activitiesService.LoadRunsAsync();
List<GarminActivity> matchedRuns = [.. allRuns.Where(r => fitActivityIds.Contains(r.ActivityId))];

Console.WriteLine($"Total activities: {allActivities.Count}");
Console.WriteLine($"Total runs: {allRuns.Count}");

