using ParkLocator.Features.Regions.Create;
using ParkLocator.Features.Regions.Retrieve;

namespace ParkLocator.Features.Regions;

public static class RoutesExtensions
{
    public static void AddRoutes(this IEndpointRouteBuilder app)
    {
        RetrievesRegions.Endpoint(app);
        CreateRegion.EndPoint(app);

    }
}
