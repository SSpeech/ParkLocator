using MediatR;

namespace ParkLocator.Features.Regions.Retrieve.Regions.RegionsEndpoint;

public static class RetrieveRegionsEndpoint
{
    public static void AddRetrieveRegionsRoute(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/region", static async (ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new Command.Command(), cancellationToken);
            return Results.Ok(result.Value);
        });
    }
}
