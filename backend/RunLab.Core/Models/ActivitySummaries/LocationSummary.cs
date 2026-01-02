namespace RunLab.Core.Models.ActivitySummaries;

public class LocationSummary
{
    public string? LocationName { get; init; }
    public GeoPoint? Start { get; init; }
    public GeoPoint? End { get; init; }
    public GeoBoundingBox? BoundingBox { get; init; }
    public bool HasLocation =>
        Start?.IsValid == true &&
        End?.IsValid == true;
}

public readonly record struct GeoPoint(double Latitude, double Longitude)
{
    public bool IsValid =>
        Latitude is >= -90 and <= 90 &&
        Longitude is >= -180 and <= 180;
}

public sealed class GeoBoundingBox
{
    public double MinLatitude { get; init; }
    public double MaxLatitude { get; init; }

    public double MinLongitude { get; init; }
    public double MaxLongitude { get; init; }

    public bool IsValid =>
        MinLatitude <= MaxLatitude &&
        MinLongitude <= MaxLongitude;
}
