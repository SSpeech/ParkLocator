using ParkLocator.Features.Regions.Create.Contracts;
using static ParkLocator.Features.Regions.Create.CreateRegion;

namespace ParkLocator.Features.Regions.Create.Extensions;

public static class CommandExtensions
{
    public static Command Map(this CreateRegionRequest request) => new(
            request.Country,
            request.Name,
            request.StateProvince,
            request.Locality
            );
}
