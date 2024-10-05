using ParkLocator.Features.Regions.Create.Contracts;

namespace ParkLocator.Features.Regions.Create.Extensions;

public static class CommandExtensions
{
    public static Command.Command Map(this CreateRegionRequest request)
    {
        return new Command.Command(
            request.Country,
            request.Name,
            request.StateProvince,
            request.Locality
            );

    }
}
