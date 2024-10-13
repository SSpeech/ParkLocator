using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkLocator.DataBase;
using ParkLocator.Entities;
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

    public async Task<Result<IEnumerable<RegionResponse>>> Handle(Command.Command request, CancellationToken cancellationToken)
    {
        List<Region>? regions = await _dbContext.Regions.AsNoTracking().ToListAsync(cancellationToken);

        IEnumerable<RegionResponse>? response = regions.ToResponse();

        return Result.Success(response);
    }
}

