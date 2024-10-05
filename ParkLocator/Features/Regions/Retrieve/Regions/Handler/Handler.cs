
using MediatR;
using ParkLocator.DataBase;
using ParkLocator.Entities.Regions;
using ParkLocator.Features.Regions.Create.Contracts;
using ParkLocator.Shared.Results;

namespace ParkLocator.Features.Regions.Retrieve.Regions.Handler;

internal sealed class Handler : IRequestHandler<Command.Command, Result<IEnumerable<RegionResponse>>>
{
    private readonly ApplicationDbContext _dbContext;
    public Handler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Result<IEnumerable<RegionResponse>>> Handle(Command.Command request, CancellationToken cancellationToken)
    {
        List<Region>? regions = [.. _dbContext.Regions];
        if (regions.Count == 0)
        {
            return Task.FromResult(Result.Success<IEnumerable<RegionResponse>>([]));
        }

        List<RegionResponse>? result = regions.Select(region => new RegionResponse(
            region.Country,
            region.Country,
            region.StateProvince,
            [],
            region.Id,
            region.Locality)).ToList();

        return Task.FromResult(Result.Success<IEnumerable<RegionResponse>>(result));
    }
}

