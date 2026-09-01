using Microsoft.Extensions.Configuration;
using RunLab.Core.Garmin;
using RunLab.Core.Metrics;
using RunLab.Core.Models;

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
    double hrDcoupling = RunMetrics.CalculatePowerDecoupling(run.FitData.Records);
    double cadenceDegradation = RunMetrics.CalculateCadenceDegradation(run.FitData.Records);
    double gradeAdjustedPace = RunMetrics.CalculateAverageGradeAdjustedPace(run.FitData.Records);
   
    Console.WriteLine($"Run: {run.ActivityId} - HR Decoupling: {hrDcoupling} - CadenceDegradation: {cadenceDegradation} - GradeAdjustedPace: {gradeAdjustedPace}");
}

