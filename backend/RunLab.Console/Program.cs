using Microsoft.Extensions.Configuration;
using RunLab.Core.Models;
using RunLab.Core.Services;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

IConfiguration config = builder.Build();

var jsonPath = config["Garmin:ActivitiesPath"];

GarminActivityService service = new (jsonPath);

List<GarminActivity> allActivities = await service.LoadActivitiesAsync();
List<GarminActivity> allRuns = await service.LoadRunsAsync();

Console.WriteLine($"Total activities: {allActivities.Count}");
Console.WriteLine($"Total runs: {allRuns.Count}");
