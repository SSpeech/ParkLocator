using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkLocator.DataBase;
using ParkLocator.Entities;
using ParkLocator.Shared;
using ParkLocator.Shared.Errors;
using ParkLocator.Shared.Paging;
using ParkLocator.Shared.Results;

namespace ParkLocator.Features.Regions.Queries;
public class GetRegion
{
    public static void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/region", static async (int page, int pageSize, ISender sender, CancellationToken cancellationToken) =>
        {
            Result<PagedList<RegionDto>> result = await sender.Send(new GetRegionQuery(page, pageSize), cancellationToken);
            return result.IsFailure ? Results.BadRequest(result.Errors.ResponseExtension(result.Error)) : Results.Ok(result.Value);
        })
        .WithTags(nameof(Region))   //TODO: This is a good practice. It's always good to specify the tags of the endpoint
        .Produces<PagedList<RegionDto>>(); //TODO: This is a good practice. It's always good to specify the return type of the endpoint
    }

    //TODO: You don't need to have validators for queries. You must perform the retrieval directly.
    //In case of nested requests (api/v1/persons/1/addresses/1) you must
    //retrieve the person with their addresses and then if it's null... 404
    public class Validator : AbstractValidator<GetRegionQuery>
    {
        public Validator()
        {
            RuleFor(query => query.Page)
                .InclusiveBetween(0, 100)   //TODO: Have a look at this inclusive between
                .NotEmpty();
            RuleFor(query => query.PageSize)
                //TODO: It's enough with a NotEmpty, which will check it's not 0
                .NotEmpty();
            //.GreaterThan(0)
            //.NotNull()
            //.NotEmpty();
        }
    }

    //TODO: Naming - Query is a very generic name. It's better to use a more descriptive name and end it with Query
    public record GetRegionQuery(int Page, int PageSize) : IRequest<Result<PagedList<RegionDto>>>;

    internal sealed class Handler : IRequestHandler<GetRegionQuery, Result<PagedList<RegionDto>>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IValidator<GetRegionQuery> _validator;
        public Handler(ApplicationDbContext dbContext, IValidator<GetRegionQuery> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<Result<PagedList<RegionDto>>> Handle(GetRegionQuery request, CancellationToken cancellationToken)
        {
            //TODO: Same, you don't need to validate queries. You must perform the retrieval directly.
            //So then, the result will be direct and no need to wrap it in a Result
            ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                List<Error>? errors = ValidationsResults<PagedList<RegionDto>>.ValidationResultErrors(validationResult);
                return ValidationsResults<PagedList<RegionDto>>.CreateErrorResult(errors,
                    "RegionsRetrieval.Validation",
                    "One or more Validation Errors occurred on during Retrieval");
            }

            //TODO: Move this into an extension method. Think that this will be a common operation
            IQueryable<Region>? regionsQuery = _dbContext.Regions.AsNoTracking().AsQueryable();
            IQueryable<RegionDto>? response = regionsQuery.ToResponse();
            var regions = await PagedList<RegionDto>.CreateAsync(response, request.Page, request.PageSize, cancellationToken);
            return Result.Success(regions);
        }
    }

}

