using ParkLocator.Entities;
using ParkLocator.Features.Regions.Create.Contracts;

namespace ParkLocator.Features.Regions.Retrieve.Regions;

public static class RegionExtensions
{
    public static RegionResponse ToResponse(this Region region) =>
        new(region.Id,
            region.Country,
            region.Name,
            region.StateProvince,
            [],
            region.Locality);

    public static IEnumerable<RegionResponse> ToResponse(this IEnumerable<Region> regions)
        => regions.Select(ToResponse);
}