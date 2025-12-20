using Dynastream.Fit;
using RunLab.Core.Models;
using System.IO.Compression;

namespace RunLab.Core.Services;

public class GarminFitParser
{
    private readonly string _fitRootFolder;

    public GarminFitParser(string fitRootFolder)
    {
        if (!Directory.Exists(fitRootFolder))
            throw new DirectoryNotFoundException($"FIT root folder not found: {fitRootFolder}");

        _fitRootFolder = fitRootFolder;
    }

    public IEnumerable<GarminFitActivity> LoadAllActivities()
    {
        foreach (string monthFolder in Directory.GetDirectories(_fitRootFolder))
        {
            foreach (string zipPath in Directory.GetFiles(monthFolder, "*.zip"))
            {
                foreach (GarminFitActivity activity in ExtractAndParseFit(zipPath))
                {
                    yield return activity;
                }
            }
        }
    }

    private IEnumerable<GarminFitActivity> ExtractAndParseFit(string zipPath)
    {
        using ZipArchive zip = ZipFile.OpenRead(zipPath);
        foreach (ZipArchiveEntry entry in zip.Entries.Where(e => e.FullName.EndsWith(".fit", StringComparison.OrdinalIgnoreCase)))
        {
            int underscoreIndex = entry.FullName.IndexOf('_');
            long id = long.TryParse(entry.FullName.AsSpan(0, underscoreIndex), out long result) ? result : 0;

            using Stream entryStream = entry.Open();
            using MemoryStream fitStream = new((int)entry.Length);

            entryStream.CopyTo(fitStream);
            fitStream.Position = 0;

            Decode decode = new();

            if (!decode.CheckIntegrity(fitStream))
            {
                Console.WriteLine($"Warning: Corrupt FIT file: {entry.FullName} in {zipPath}");
                continue;
            }

            fitStream.Seek(0, SeekOrigin.Begin);

            GarminFitActivity activity = new();
            activity.Id = id;

            decode.MesgEvent += (sender, e) =>
            {
                switch (e.mesg.Num)
                {
                    case MesgNum.Session:
                        SessionMesg session = new(e.mesg);
                        activity.StartTime = session.GetStartTime() ?? default;
                        activity.Duration = session.GetTotalElapsedTime() ?? 0;
                        activity.Distance = session.GetTotalDistance() ?? 0;
                        activity.AvgHr = session.GetAvgHeartRate() ?? 0;
                        activity.MaxHr = session.GetMaxHeartRate() ?? 0;
                        break;

                    case MesgNum.Record:
                        RecordMesg recordMesg = new(e.mesg);
                        GarminFitRecord record = new()
                        {
                            Timestamp = recordMesg.GetTimestamp() ?? default,
                            HeartRate = recordMesg.GetHeartRate() ?? 0,
                            Cadence = recordMesg.GetCadence() ?? 0,
                            Power = recordMesg.GetPower() ?? 0,
                            Latitude = (recordMesg.GetPositionLat() ?? 0) / 1e7,
                            Longitude = (recordMesg.GetPositionLong() ?? 0) / 1e7,
                            Altitude = recordMesg.GetEnhancedAltitude() ?? 0
                        };
                        activity.Records.Add(record);
                        break;
                }
            };

            decode.Read(fitStream);
            yield return activity;
        }
    }
}