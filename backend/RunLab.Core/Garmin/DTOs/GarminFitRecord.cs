using RunLab.Core.Models;

namespace RunLab.Core.Garmin.DTOs;

public sealed class GarminFitRecord
{
    public Dynastream.Fit.DateTime? Timestamp { get; init; }
    public byte HeartRate { get; init; }
    public byte Cadence { get; init; }
    public ushort Power { get; init; }
    public float Altitude { get; init; }
    public GeoPosition? GeoPosition { get; init; }
}
