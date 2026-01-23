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

foreach (GarminRunActivity run in allRuns)
{
    double HRDcoupling = FitCalculationService.CalculatePowerDecoupling(run.FitData.Records);
    double CadenceDegradation = FitCalculationService.CalculateCadenceDegradation(run.FitData.Records);
    double GradeAdjustedPace = FitCalculationService.CalculateAverageGradeAdjustedPace(run.FitData.Records);
    Console.WriteLine($"Run: {run.ActivityId} - HR Decoupling: {HRDcoupling} - CadenceDegradation: {CadenceDegradation} - GradeAdjustedPace: {GradeAdjustedPace}");
}

