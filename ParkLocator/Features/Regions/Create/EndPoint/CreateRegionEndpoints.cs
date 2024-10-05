using MediatR;
using ParkLocator.Features.Regions.Create.Contracts;
using ParkLocator.Features.Regions.Create.Extensions;

namespace ParkLocator.Features.Regions.Create.EndPoint;

public static class CreateRegionEndpoints
{
    public static void AddCreateRegionRoute(this IEndpointRouteBuilder app)
    {

        app.MapPost("api/region", async (CreateRegionRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = request.Map();
            var result = await sender.Send(command,cancellationToken);
            return result.IsFailure ? Results.BadRequest(result.ResponseExtension()) : Results.Ok(result.Value);
        });
    }
}