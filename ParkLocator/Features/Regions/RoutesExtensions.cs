using ParkLocator.Features.Regions.Commands;
using ParkLocator.Features.Regions.Queries;

namespace ParkLocator.Features.Regions;

public static class RoutesExtensions
{
    public static void AddRoutes(this IEndpointRouteBuilder app)
    {
        GetRegion.Endpoint(app);
        CreateRegion.EndPoint(app);

    }
}
