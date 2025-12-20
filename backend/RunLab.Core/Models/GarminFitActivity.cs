namespace RunLab.Core.Models;

public class GarminFitActivity
{
    public long Id { get; set; }
    public Dynastream.Fit.DateTime? StartTime { get; set; }
    public double Duration { get; set; }
    public double Distance { get; set; }
    public byte AvgHr { get; set; }
    public byte MaxHr { get; set; }

    public List<GarminFitRecord> Records { get; set; } = [];
}