namespace ParkLocator.Features.Regions.Create.Contracts;

public record CreateRegionRequest
{
    public string Country { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string StateProvince { get; set; } = string.Empty;
    public string Locality { get; set; } = string.Empty;
}
