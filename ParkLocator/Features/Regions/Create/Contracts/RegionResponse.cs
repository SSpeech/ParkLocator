using ParkLocator.Entities;

namespace ParkLocator.Features.Regions.Create.Contracts;

public record RegionResponse(Guid Id,
    string Country,
    string Name,
    string StateProvince,
    ICollection<District> Districts,
    string Locality);
