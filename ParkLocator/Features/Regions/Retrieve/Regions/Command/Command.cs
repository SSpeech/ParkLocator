using MediatR;
using ParkLocator.Entities.Regions;
using ParkLocator.Features.Regions.Create.Contracts;
using ParkLocator.Shared.Results;

namespace ParkLocator.Features.Regions.Retrieve.Regions.Command;

public class Command : IRequest<Result<IEnumerable<RegionResponse>>>
{ }
