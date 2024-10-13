using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkLocator.DataBase;
using ParkLocator.Entities;
using ParkLocator.Features.Regions.Create.Contracts;
using ParkLocator.Features.Regions.Create.Extensions;
using ParkLocator.Shared;
using ParkLocator.Shared.Errors;
using ParkLocator.Shared.Results;

namespace ParkLocator.Features.Regions.Create;
public static class CreateRegionEndpoints
{
    public static void AddCreateRegionRoute(this IEndpointRouteBuilder app)
    {

        app.MapPost("api/region", async (CreateRegionRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = request.Map();
            var result = await sender.Send(command, cancellationToken);
            return result.IsFailure ? Results.BadRequest(result.ResponseExtension()) : Results.Ok(result.Value);
        });
    }
}

public class CreateRegion
{
    public class Command : IRequest<Result<Guid>>
    {
        public Command(string country, string name, string stateProvince, string locality)
        {
            Country = country;
            Name = name;
            StateProvince = stateProvince;
            Locality = locality;
        }
        public string Country { get; private set; }
        public string Name { get; private set; }
        public string StateProvince { get; }
        public string Locality { get; private set; }
    }
    public class Validator : AbstractValidator<Command>
    {
        private readonly ApplicationDbContext _dbContext;


        public Validator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(200)
                .MustAsync(async (cmd, name, ct) => !await _dbContext.Set<Region>()
                    .AsNoTracking()
                    .AnyAsync(x => x.Name == cmd.Name, ct))
               .WithMessage("A region with the same name already exists.");

            RuleFor(c => c.Country)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(200);

            RuleFor(c => c.StateProvince)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(200);

            RuleFor(c => c.Locality)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(200);
        }
    }
    internal sealed class Handler : IRequestHandler<Command, Result<Guid>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IValidator<Command> _validator;
        public Handler(ApplicationDbContext dbContext, IValidator<Command> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }
        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                List<Error> errors = ValidationsResults<Guid>.ValidationResultErrors(validationResult);
                return ValidationsResults<Guid>.CreateErrorResult(errors,
                    "CreateRegion.Validation",
                    "One or more Validation Errors occurred on the creation");
            }

            Region regionCreated = await AddRegionAsync(request, cancellationToken);
            return regionCreated.Id;
        }

        private async Task<Region> AddRegionAsync(Command request, CancellationToken cancellationToken)
        {
            Region region = new([],
                                request.Country,
                                request.Name,
                                request.StateProvince,
                                request.Locality);

            _dbContext.Add(region);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return region;
        }
    }

}