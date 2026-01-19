using Microsoft.Extensions.Configuration;
using RunLab.Core.Models;
using RunLab.Core.Services;

IConfigurationBuilder builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

IConfiguration config = builder.Build();

string jsonPath = config["Garmin:ActivitiesPath"] ?? default!;
string fitFileRoot = config["Garmin:FitFilesRoot"] ?? default!;

GarminActivityService activitiesService = new (jsonPath, fitFileRoot);
List<GarminRunActivity> allRuns = await activitiesService.LoadRunsAsync();

FitCalculationService calculator = new();

foreach (GarminRunActivity run in allRuns)
{
    double HRDcoupling = calculator.CalculatePowerDecoupling(run.FitData.Records);
    Console.WriteLine($"Run: {run.ActivityId} - HR Decoupling: {HRDcoupling}");
}

