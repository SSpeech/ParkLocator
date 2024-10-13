using ParkLocator.Entities;
using ParkLocator.Features.Regions.Create.Contracts;

namespace ParkLocator.Features.Regions.Retrieve;

public static class RegionsExtensions
{
    public static RegionResponse ToResponse(this Region region) =>
        new(region.Id,
            region.Country,
            region.Name,
            region.StateProvince,
            [],
            region.Locality);

    public static IQueryable<RegionResponse> ToResponse(this IQueryable<Region> regions)
        => regions.Select(region => region.ToResponse());
}