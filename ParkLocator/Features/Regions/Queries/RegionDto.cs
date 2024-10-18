using ParkLocator.Entities;

namespace ParkLocator.Features.Regions.Queries;

//TODO: This was located with the Commands and it's a Query Response.
//I would suggest naming it RegionDto instead RegionResponse
public record RegionDto(Guid Id,
    string Country,
    string Name,
    string StateProvince,
    ICollection<District> Districts,
    string Locality);
