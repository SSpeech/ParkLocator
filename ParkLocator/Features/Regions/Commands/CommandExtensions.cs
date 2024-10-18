using static ParkLocator.Features.Regions.Commands.CreateRegion;

namespace ParkLocator.Features.Regions.Commands;

public static class CommandExtensions
{
    public static CreateRegionCommand Map(this RegionCreateDto dto) =>
        new(dto.Country,
            dto.Name,
            dto.StateProvince,
            dto.Locality);
}
