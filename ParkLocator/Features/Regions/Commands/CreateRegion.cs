using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkLocator.DataBase;
using ParkLocator.Entities;
using ParkLocator.Features.Regions.Queries;
using ParkLocator.Shared;
using ParkLocator.Shared.Results;

namespace ParkLocator.Features.Regions.Commands;

public class CreateRegion
{
    public static void EndPoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/region", async (RegionCreateDto request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = request.Map();
            var result = await sender.Send(command, cancellationToken);
            return result.IsFailure
                ? Results.BadRequest(result.Errors.ResponseExtension(result.Error))
                : Results.Ok(result.Value);
        })
        .WithTags(nameof(Region))   //TODO: This is a good practice. It's always good to specify the tags of the endpoint
        .Produces<Guid>();    //TODO: This is a good practice. It's always good to specify the return type of the endpoint
    }

    //TODO: I moved this here instead of inside a dedicated file. I suggest following this naming
    public record RegionCreateDto(string Country, string Name, string StateProvince, string Locality);
    //TODO: Naming. Command always should end with Command
    public record CreateRegionCommand(string Country, string Name, string StateProvince, string Locality) : IRequest<Result<Guid>>;

    //TODO: Naming. Validator always should end with Validator
    public class CreateRegionCommandValidator : AbstractValidator<CreateRegionCommand>
    {
        public CreateRegionCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(c => c.Name)
                .NotEmpty() //TODO: Not Empty and NotNull are similar but not the same. NotEmpty is a combination of NotNull and NotEmpty. Look at its documentation.
                .Length(3, 200) //Easier way to define the min and max length of a string
                .MustAsync(async (name, ct) => !await dbContext.Set<Region>()
                                                                        .AsNoTracking()
                                                                        .AnyAsync(x => x.Name == name, ct))
               .WithMessage("A region with the same name already exists.");

            RuleFor(c => c.Country)
                .NotEmpty()
                .Length(3, 200);

            RuleFor(c => c.StateProvince)
                .NotEmpty()
                .Length(3, 200);

            RuleFor(c => c.Locality)
                .NotEmpty()
                .Length(3, 200);
        }
    }
    internal sealed class Handler : IRequestHandler<CreateRegionCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IValidator<CreateRegionCommand> _validator;
        public Handler(ApplicationDbContext dbContext, IValidator<CreateRegionCommand> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }
        public async Task<Result<Guid>> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
        {
            //TODO: Use validationpipelines instead of manually doing this on the handler.
            //That way, you will enter into the Handler only if the request is valid so it's a safe place
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = ValidationsResults<Guid>.ValidationResultErrors(validationResult);
                return ValidationsResults<Guid>.CreateErrorResult(errors,
                    "CreateRegion.Validation",
                    "One or more Validation Errors occurred on the creation");
            }

            var regionCreated = await AddRegionAsync(request, cancellationToken);
            return regionCreated.Id;
        }

        private async Task<Region> AddRegionAsync(CreateRegionCommand request, CancellationToken cancellationToken)
        {
            //TODO: If by default you're creating a region without linking it to any district, then you should remove the empty list from the constructor
            //In case in a future you create a region along a district you can create a new constructor with the list of districts
            Region item = new(request.Country,
                                request.Name,
                                request.StateProvince,
                                request.Locality);

            _dbContext.Set<Region>().Add(item);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return item;
        }
    }

}