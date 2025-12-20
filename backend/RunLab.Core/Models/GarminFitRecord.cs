namespace RunLab.Core.Models;

public class GarminFitRecord
{
    public Dynastream.Fit.DateTime? Timestamp { get; set; }
    public byte HeartRate { get; set; }
    public byte Cadence { get; set; }
    public ushort Power { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public float Altitude { get; set; }
}
