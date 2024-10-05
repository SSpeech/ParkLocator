using ParkLocator.Entities.Districts;

namespace ParkLocator.Features.Regions.Create.Contracts;

public class RegionResponses
{
    public string Country { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string StateProvince { get; set; } = string.Empty;
    public ICollection<District> Districts { get; set; } = [];
    public Guid Id { get; set; }
    public string Locality { get; set; } = string.Empty;
}

public record RegionResponse(string Country, string Name, string StateProvince, ICollection<District> Districts, Guid Id, string Locality)
{ }
