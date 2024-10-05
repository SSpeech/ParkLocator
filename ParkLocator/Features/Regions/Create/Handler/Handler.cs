using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ParkLocator.DataBase;
using ParkLocator.Entities.Regions;
using ParkLocator.Shared;
using ParkLocator.Shared.Errors;
using ParkLocator.Shared.Results;

namespace ParkLocator.Features.Regions.Create.Handler;

internal sealed class Handler : IRequestHandler<Command.Command, Result<Guid>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<Command.Command> _validator;
    public Handler(ApplicationDbContext dbContext, IValidator<Command.Command> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    public async Task<Result<Guid>> Handle(Command.Command request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
        {
            List<Error> errors = ValidationsResults<Guid>.ValidationResultErrors(validationResult);
            return ValidationsResults<Guid>.CreateErrorResult(errors,
                "CreateRegion.Validation",
                "One or more Validation Errors occurred on the creation");
        }

        Region? region = _dbContext.Regions.FirstOrDefault(region => region.Name.Equals(request.Name));

        if (region is not null)
        {
            return ValidationsResults<Guid>.CreateErrorResult([],
                "CreateRegion.AlreadyExists",
                "A region with the same name already exists.");
        }
        Region regionCreated = await AddRegionAsync(request, cancellationToken);
        return regionCreated.Id;
    }

    private async Task<Region> AddRegionAsync(Command.Command request, CancellationToken cancellationToken)
    {
        Region region = new Region.Builder()
                            .WithDistrict([])
                            .WithLocality(request.Locality)
                            .WithCountry(request.Country)
                            .WithSateProvince(request.StateProvince)
                            .WithName(request.Name)
                            .Build();

        _dbContext.Add(region);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return region;
    }
}

