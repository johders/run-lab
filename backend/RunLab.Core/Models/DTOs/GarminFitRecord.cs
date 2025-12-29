namespace RunLab.Core.Models.DTOs;

public sealed class GarminFitRecord
{
    public Dynastream.Fit.DateTime? Timestamp { get; init; }
    public byte HeartRate { get; init; }
    public byte Cadence { get; init; }
    public ushort Power { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public float Altitude { get; init; }
}
