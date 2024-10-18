using ParkLocator.Entities;

namespace ParkLocator.Features.Regions.Queries;

public static class RegionsExtensions
{
    public static RegionDto ToResponse(this Region region) =>
        new(region.Id,
            region.Country,
            region.Name,
            region.StateProvince,
            [],
            region.Locality);

    public static IQueryable<RegionDto> ToResponse(this IQueryable<Region> regions)
        => regions.Select(region => region.ToResponse());
}